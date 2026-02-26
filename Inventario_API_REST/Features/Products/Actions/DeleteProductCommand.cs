using Inventario_API_REST.Database;
using Microsoft.EntityFrameworkCore;

namespace Inventario_API_REST.Features.Products;

using Response = Result<int>;
public record DeleteProductCommand(int Id) : IRequest<Response>;
public class DeleteProductHanlder(InventoryDbContext dbContext) : IHandler<DeleteProductCommand, Response>
{
    public async Task<Response> Handle(DeleteProductCommand request, CancellationToken cancellationToken = default)
    {
        return await AsyncHandler.TryCatchAsync(async () =>
        {
            if (request.Id == 0)
                return Response.Failure($"Please insert a valid Id", 400);

            var product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (product is null)
                return Response.Failure($"Product not found ({request.Id})", 404);

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Response.Ok(request.Id, "The product was deleted!");
        }, (ex) => Response.Failure($"An error occurred while creating the product: {ex.Message}", 500));
    }
}