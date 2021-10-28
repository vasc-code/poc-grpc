using Application.Queries.Grpc;
using Application.Queries.Grpc.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Bootstrap
{
    internal static class ApplicationSetup
    {
        internal static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IGrpcQuery, GrpcQuery>();
        }
    }
}