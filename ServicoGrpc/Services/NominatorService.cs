using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ServicoGrpc
{
    public class NominatorService : Nominator.NominatorBase
    {
        private readonly ILogger<NominatorService> _logger;
        public NominatorService(ILogger<NominatorService> logger)
        {
            _logger = logger;
        }

        public override Task<NameReply> GetFullName(NameRequest request, ServerCallContext context)
        {
            return Task.FromResult(new NameReply
            {
                Message = $"{request.FirstName} {request.LastName}"
            });
        }
    }
}
