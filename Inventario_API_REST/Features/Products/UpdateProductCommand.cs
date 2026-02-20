using Inventario_API_REST.Database;
using Inventario_API_REST.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventario_API_REST.Features.Products;

using Response = Result<UpdateProductResponse>;

public record UpdateProductResponse(int Id, string Name, string Description, int Stock, decimal Cost, decimal Price, decimal EarningUnit);
public record UpdateProductCommand(int Id, string Name, string Description, int Stock, decimal Cost, decimal Price) : IRequest<Response>;
public class UpdateProductHandler(InventoryDbContext _dbContext) : IHandler<UpdateProductCommand, Response>
{
    public async Task<Response> Handle(UpdateProductCommand request, CancellationToken ct = default)
    {
        return await AsyncHandler.TryCatchAsync(async () =>
        {
            if (request.Price < request.Cost)
                return Response.Failure("The selling price cannot be lower than the cost.", 400);

            var product = await _dbContext.Products
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (product == null)
                return Response.Failure($"Product with ID {request.Id} not found.", 404);

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Stock = request.Stock;
            product.Cost = request.Cost;

            await _dbContext.SaveChangesAsync(ct);

            return Response.Ok(MapToResponse(product));
        }, (ex) => Response.Failure($"Error updating product: {ex.Message}", 500));
    }

    private static UpdateProductResponse MapToResponse(Product p) =>
        new(p.Id, p.Name, p.Description, p.Stock, p.Cost, p.Price, p.Price - p.Cost);
}