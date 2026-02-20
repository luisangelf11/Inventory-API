using Inventario_API_REST.Database;
using Microsoft.EntityFrameworkCore;

namespace Inventario_API_REST.Features.Roles;

using Response = Result<List<RoleDto>>;
public record RoleDto(int Id, string Name);
public record GetRolesQuery : IRequest<Response>;

public class RoleQueryHandler(InventoryDbContext _dbcontext) : IHandler<GetRolesQuery, Response>
{
    public async Task<Response> Handle(GetRolesQuery request, CancellationToken cancellationToken = default)
    {
        return await AsyncHandler.TryCatchAsync(async () =>
        {
            var roles = await _dbcontext.Roles
                    .AsNoTracking()
                    .Select(r => new RoleDto(r.Id, r.Name))
                    .ToListAsync(cancellationToken);

            return Response.Ok(roles);
        }, (ex) => Response.Failure($"Error loading roles: {ex.Message}", 500));
    }
}