using Polly.Retry;

namespace Infrastructure.Middlewares.Retry.Interface
{
    public interface IRetryMiddleware
    {
        AsyncRetryPolicy RetryException { get; }
        void ConfigureRetryException(int retryCount, int nextRetryInSeconds, string retryContext);
    }
}