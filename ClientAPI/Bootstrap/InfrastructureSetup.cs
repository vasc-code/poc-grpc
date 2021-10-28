using Infrastructure.Messages;
using Infrastructure.Messages.Interfaces;
using Infrastructure.Middlewares.Retry;
using Infrastructure.Middlewares.Retry.Interface;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bootstrap
{
    internal static class InfrastructureSetup
    {
        internal static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRetryMiddleware, RetryMiddleware>();
            services.AddScoped<IMessagesHandler, MessagesHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        }
    }
}