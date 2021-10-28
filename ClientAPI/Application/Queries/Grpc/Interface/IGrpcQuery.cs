using Application.Boundaries.Grpc.GetFullName;
using Application.Boundaries.Grpc.SayHello;
using System.Threading.Tasks;

namespace Application.Queries.Grpc.Interface
{
    public interface IGrpcQuery
    {
        Task<SayHelloOutput> SayHelloAsync(SayHelloInput input);
        Task<GetFullNameOutput> GetFullNameAsync(GetFullNameInput input);
    }
}
