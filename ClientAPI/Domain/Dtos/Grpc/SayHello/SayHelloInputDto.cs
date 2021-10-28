namespace Domain.Dtos.Grpc.SayHello
{
    public class SayHelloInputDto
    {

        public SayHelloInputDto(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
