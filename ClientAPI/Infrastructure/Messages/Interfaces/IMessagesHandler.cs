using System.Threading.Tasks;

namespace Infrastructure.Messages.Interfaces
{
    public interface IMessagesHandler
    {
        Task<TResponse> SendCommandAsync<TCommand, TResponse>(TCommand command) where TCommand : Command<TResponse>;
        Task SendDomainNotificationAsync<TDomainNotification>(TDomainNotification notificacao) where TDomainNotification : DomainNotification;
    }
}