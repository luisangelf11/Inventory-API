using Inventario_API_REST.Database;
using Microsoft.EntityFrameworkCore;

namespace Inventario_API_REST.Features.Products;

using Response = ResultPaginated<ProductDto>;

public record ProductDto(int Id, string Name, string Description, int Stock, decimal Price, string CreatedBy);
public record GetProductsQuery(int PageNumber, int PageSize) : IRequest<Response>;
public class GetProductsHandler(InventoryDbContext _dbContext) : IHandler<GetProductsQuery, Response>
{
    public async Task<Response> Handle(GetProductsQuery request, CancellationToken ct)
    {
        return await AsyncHandler.TryCatchAsync(async () =>
        {
            if (request.PageNumber < 1 || request.PageSize < 1) 
                return Response.Failure("The current page cannot be less than 1 and the size cannot be less than 1.");

            var totalCount = await _dbContext.Products.CountAsync();
            var items = await _dbContext.Products
                .AsNoTracking()
                .Include(p => p.CreatedBy)
                .OrderByDescending(p => p.Id).Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(p => new ProductDto(
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Stock,
                    p.Price,
                    p.CreatedBy.Username))
                .ToListAsync(ct);

            return Response.Ok(items, totalCount, request.PageNumber, request.PageSize);
        }, (ex) => Response.Failure($"Error retrieving products: {ex.Message}", 500));
    }
}