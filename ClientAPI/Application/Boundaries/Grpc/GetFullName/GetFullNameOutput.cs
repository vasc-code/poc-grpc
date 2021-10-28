namespace Application.Boundaries.Grpc.GetFullName
{
    public class GetFullNameOutput
    {
        public GetFullNameOutput(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
