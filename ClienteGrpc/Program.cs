using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace ClienteGrpc
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var greeter = new Greeter.GreeterClient(channel);
            var nominator = new Nominator.NominatorClient(channel);

            var opcao = "";
            while (opcao != "S")
            {
                Console.Write("Digite uma opção - 1: SayHello / 2:GetFullName / S: Sair -> ");
                opcao = Console.ReadLine().ToUpper();

                switch (opcao)
                {
                    case "1": 
                        await SayHello(greeter);
                        break;

                    case "2":
                        await GetFullName(nominator);
                        break;

                    default:
                        break;
                }

                Console.WriteLine("\n");
            }
        }

        private static async Task SayHello(Greeter.GreeterClient client)
        {
            var reply = await client.SayHelloAsync(
                new HelloRequest
                {
                    Name = "POC - GRPC"
                }
            );

            Console.WriteLine("Saudação: " + reply.Message);
        }        
        
        private static async Task GetFullName(Nominator.NominatorClient client)
        {
            var reply = await client.GetFullNameAsync(
                new NameRequest
                {
                    LastName = "Silva",
                    FirstName = "João"
                }
            );

            Console.WriteLine("Nome Completo: " + reply.Message);
        }
    }
}
