using Application.Boundaries.Grpc.GetFullName;
using Application.Boundaries.Grpc.SayHello;
using Application.Queries.Grpc.Interface;
using Application.Queries.Grpc.Mappers;
using ClienteGrpc;
using Domain.Dtos.Grpc.GetFullName;
using Domain.Dtos.Grpc.SayHello;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Application.Queries.Grpc
{
    public class GrpcQuery : IGrpcQuery
    {
        private readonly GrpcChannel _grpcChannel;
        private readonly Greeter.GreeterClient _greeter;
        private readonly Nominator.NominatorClient _nominator;

        public GrpcQuery(IConfiguration configuration)
        {
            _grpcChannel = GrpcChannel.ForAddress(configuration.GetSection("URL_SERVICO_GRPC").Value);
            _greeter = new Greeter.GreeterClient(_grpcChannel);
            _nominator = new Nominator.NominatorClient(_grpcChannel);
        }

        public async Task<SayHelloOutput> SayHelloAsync(SayHelloInput input)
        {
            var inputDto = input.MapToSayHelloInputDto();

            var output = await SayHello(_greeter, inputDto).ConfigureAwait(false);

            return new SayHelloOutput(
                output
            );
        }

        public async Task<GetFullNameOutput> GetFullNameAsync(GetFullNameInput input)
        {
            var inputDto = input.MapToGetFullNameInputDto();

            var output = await GetFullName(_nominator, inputDto).ConfigureAwait(false);

            return new GetFullNameOutput(
                output
            );
        }

        private async Task<string> SayHello(Greeter.GreeterClient client, SayHelloInputDto input)
        {
            var reply = await client.SayHelloAsync(
                new HelloRequest
                {
                    Name = input.Name
                }
            );

            return "Saudação: " + reply.Message;
        }

        private async Task<string> GetFullName(Nominator.NominatorClient client, GetFullNameInputDto input)
        {
            var reply = await client.GetFullNameAsync(
                new NameRequest
                {
                    LastName = input.LastName,
                    FirstName = input.FirstName
                }
            );

            return "Nome Completo: " + reply.Message;
        }
    }
}
