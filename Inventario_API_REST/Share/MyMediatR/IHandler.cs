namespace Inventario_API_REST.Share.MyMediatR
{
    public interface IRequest<TResponse> { }
    public interface IHandler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default);
    }
}
