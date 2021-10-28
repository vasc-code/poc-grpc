using Application.Boundaries.Grpc.GetFullName;
using Application.Boundaries.Grpc.SayHello;
using Domain.Dtos.Grpc.GetFullName;
using Domain.Dtos.Grpc.SayHello;

namespace Application.Queries.Grpc.Mappers
{
    internal static class TraceMapper
    {
        internal static SayHelloInputDto MapToSayHelloInputDto(this SayHelloInput input)
        {
            return new SayHelloInputDto(
                input.Name
            );
        }

        internal static GetFullNameInputDto MapToGetFullNameInputDto(this GetFullNameInput input)
        {
            return new GetFullNameInputDto(
                input.LastName,
                input.FirstName
            );
        }
    }
}
