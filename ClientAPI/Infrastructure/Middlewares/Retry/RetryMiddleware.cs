using Infrastructure.Middlewares.Retry.Interface;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;

namespace Infrastructure.Middlewares.Retry
{
    public class RetryMiddleware : IRetryMiddleware
    {
        private readonly ILogger<RetryMiddleware> _logger;

        public RetryMiddleware(ILogger<RetryMiddleware> logger)
        {
            _logger = logger;
        }

        public AsyncRetryPolicy RetryException { get; private set; }

        public void ConfigureRetryException(int retryCount, int nextRetryInSeconds, string retryContext)
        {
            RetryException = Policy.Handle<Exception>().WaitAndRetryAsync
            (
                retryCount,
                sleepDurationProvider => TimeSpan.FromSeconds(nextRetryInSeconds),
                (exception, timeSpan, retryCount, context) =>
                {
                    _logger.LogError
                    (
                        exception,
                        $"Erro em {retryContext} - Tentativa {retryCount} - Mensagem -> {exception.Message}"
                    );
                }
            );
        }
    }
}