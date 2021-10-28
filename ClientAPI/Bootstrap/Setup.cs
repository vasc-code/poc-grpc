using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bootstrap
{
    public static class Setup
    {
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwagger();
            services.AddInfrastructure(configuration);
            services.AddApplication();
        }

        public static void UseSwaggerRastreabilidade(this IApplicationBuilder builder)
        {
            builder.ConfigureSwagger();
        }
    }
}