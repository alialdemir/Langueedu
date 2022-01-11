using MediatR;

namespace Langueedu.UnitTests;

public class NoOpMediator : IMediator
{
  public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
  {
    return (IAsyncEnumerable<TResponse>)Task.FromResult<IAsyncEnumerable<TResponse?>>(default);

  }

  public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
  {
    return (IAsyncEnumerable<object?>)Task.FromResult<IAsyncEnumerable<object?>>(default);
  }

  public Task Publish(object notification, CancellationToken cancellationToken = default)
  {
    return Task.CompletedTask;
  }

  public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
  {
    return Task.CompletedTask;
  }

  public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
  {
    return Task.FromResult<TResponse>(default);
  }

  public Task<object?> Send(object request, CancellationToken cancellationToken = default)
  {
    return Task.FromResult<object?>(default);
  }
}

