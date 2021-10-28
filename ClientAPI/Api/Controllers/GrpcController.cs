using Application.Boundaries.Grpc.GetFullName;
using Application.Boundaries.Grpc.SayHello;
using Application.Queries.Grpc.Interface;
using Infrastructure.Messages;
using Infrastructure.Messages.Interfaces;
using Infrastructure.Middlewares.Retry.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class GrpcController : BaseController<GrpcController>
    {
        private readonly IRetryMiddleware _retry;
        private readonly int _retryCount;
        private readonly int _nextRetryInSeconds;
        private readonly IGrpcQuery _grpcQuery;
        private readonly IMessagesHandler _messagesHandler;

        public GrpcController(INotificationHandler<DomainNotification> notifications,
                              IMessagesHandler messagesHandler,
                              IRetryMiddleware retry,
                              IConfiguration configuration,
                              IGrpcQuery grpcQuery) : base(notifications)
        {
            _messagesHandler = messagesHandler;
            _retry = retry;
            _retryCount = Convert.ToInt32(configuration.GetSection("RetryCount").Value);
            _nextRetryInSeconds = Convert.ToInt32(configuration.GetSection("NextRetryInSeconds").Value);
            _grpcQuery = grpcQuery;
        }

        [HttpGet("SayHello")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SayHelloOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SayHello([FromQuery] SayHelloInput input)
        {
            ConfigureRetry(nameof(SayHello));

            var output = await SayHelloAsync(input).ConfigureAwait(false);

            if (IsValidOperation())
            {
                return StatusCode(StatusCodes.Status200OK, output);
            }

            return StatusCode(StatusCodes.Status400BadRequest, GetErrorMessages());
        }

        [HttpGet("GetFullName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFullNameOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFullName([FromQuery] GetFullNameInput input)
        {
            ConfigureRetry(nameof(GetFullName));

            var output = await GetFullNameAsync(input).ConfigureAwait(false);

            if (IsValidOperation())
            {
                return StatusCode(StatusCodes.Status200OK, output);
            }

            return StatusCode(StatusCodes.Status400BadRequest, GetErrorMessages());
        }

        #region Private
        private async Task<SayHelloOutput> SayHelloAsync(SayHelloInput input)
        {
            return await _retry.RetryException.ExecuteAsync
            (
                async () => await _grpcQuery.SayHelloAsync(input)
                                            .ConfigureAwait(false)
            ).ConfigureAwait(false);
        }
        
        private async Task<GetFullNameOutput> GetFullNameAsync(GetFullNameInput input)
        {
            return await _retry.RetryException.ExecuteAsync
            (
                async () => await _grpcQuery.GetFullNameAsync(input)
                                            .ConfigureAwait(false)
            ).ConfigureAwait(false);
        }

        private void ConfigureRetry(string retryContext)
        {
            _retry.ConfigureRetryException
            (
                _retryCount,
                _nextRetryInSeconds,
                retryContext
            );
        }
        #endregion
    }
}
