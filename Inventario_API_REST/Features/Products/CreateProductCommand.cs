using Inventario_API_REST.Database;
using Inventario_API_REST.Database.Models;
using Microsoft.EntityFrameworkCore;
namespace Inventario_API_REST.Features.Products;

using Response = Result<ProductResponse>;

public record ProductResponse(int Id, string Name, string Description, int Stock, decimal Cost, decimal Price, decimal EarningUnit);
public record CreateProductCommand(string Name, string Description, int Stock, decimal Cost, decimal Price, int CreatedById) : IRequest<Response>;

public class CreateProductHandler(InventoryDbContext _dbContext) : IHandler<CreateProductCommand, Response>
{
    public async Task<Response> Handle(CreateProductCommand request, CancellationToken cancellationToken = default)
    {
        return await AsyncHandler.TryCatchAsync(async () =>
        {
            if (request.Price < request.Cost)
                return Response.Failure("The selling price cannot be less than the cost.", 400);

            if (request.Stock < 0)
                return Response.Failure("Initial stock cannot be negative.", 400);

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Stock = request.Stock,
                Cost = request.Cost,
                Price = request.Price,
                CreatedById = request.CreatedById,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var response = new ProductResponse(
                product.Id,
                product.Name,
                product.Description,
                product.Stock,
                product.Cost,
                product.Price,
                product.EarningUnit
            );
            return Response.Ok(response, 201);
        }, (ex) => Response.Failure($"An error occurred while creating the product: {ex.Message}", 500));

    }
}

