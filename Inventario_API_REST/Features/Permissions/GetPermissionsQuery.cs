using Inventario_API_REST.Database;
using Microsoft.EntityFrameworkCore;

namespace Inventario_API_REST.Features.Permissions;

using Response = Result<List<PermissionsDto>>;

public record PermissionsDto(int Id, string Name);
public record GetPermissionsQuery : IRequest<Response>;

public class PermissionsHandler(InventoryDbContext _dbContext) : IHandler<GetPermissionsQuery, Response>
{
    public async Task<Response> Handle(GetPermissionsQuery request, CancellationToken cancellationToken = default)
    {
        return await AsyncHandler.TryCatchAsync(async () =>
        {
            var permissions = await _dbContext.Permissions
                .AsNoTracking()
                .Select(p => new PermissionsDto(p.Id, p.Name))
                .ToListAsync(cancellationToken);

            return Response.Ok(permissions);
        }, (ex) => Response.Failure($"Error loading permissions: {ex.Message}", 500));
    }
}