namespace Inventario_API_REST.Share.MyMediatR
{
    public interface IMediatR
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    }
    public class MediatR(IServiceProvider _serviceProvider) : IMediatR
    {
        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var requestType = request.GetType();
            var handlerType = typeof(IHandler<,>).MakeGenericType(requestType, typeof(TResponse));

            var handler = _serviceProvider.GetService(handlerType);

            if (handler == null)
                throw new Exception($"The handler for {requestType.Name} is not found");

            var method = handlerType.GetMethod("Handle");
            if (method == null)
                throw new Exception($"The method Handle for {requestType.Name} is not found");

            var task = method.Invoke(handler, new object[] { request, cancellationToken }) as Task<TResponse>;

            if (task == null)
                throw new Exception("Error to invoke handle: Task was null");

            return await task;
        }
    }
}
