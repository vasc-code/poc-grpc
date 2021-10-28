namespace Domain.Dtos.Grpc.GetFullName
{
    public class GetFullNameInputDto
    {
        public GetFullNameInputDto(string lastName,
                                   string firstName)
        {
            LastName = lastName;
            FirstName = firstName;
        }

        public string LastName { get; }
        public string FirstName { get; }
    }
}
