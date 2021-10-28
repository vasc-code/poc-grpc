namespace Application.Boundaries.Grpc.SayHello
{
    public class SayHelloOutput
    {
        public SayHelloOutput(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
