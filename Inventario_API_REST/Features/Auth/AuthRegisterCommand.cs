using Inventario_API_REST.Database;
using Inventario_API_REST.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventario_API_REST.Features.Auth;

using Response = Result<int>;
public record AuthRegisterCommand(string Username, string Password, string Permissions) : IRequest<Response>;
public class AuthRegisterHandler(InventoryDbContext dbContext) : IHandler<AuthRegisterCommand, Response>
{
    public async Task<Response> Handle(AuthRegisterCommand request, CancellationToken cancellationToken = default)
    {
        return await AsyncHandler.TryCatchAsync(async () =>
        {
            var userExists = await dbContext.Users
           .AnyAsync(u => u.Username == request.Username, cancellationToken);

            if (userExists)
                return Response.Failure("This username already exists. Try again!", 400);

            var passHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new User
            {
                Username = request.Username,
                Password = passHash,
                RoleId = RolesId.Seller,
                Permissions = request.Permissions,
            };
            dbContext.Users.Add(newUser);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Response.Ok(newUser.Id, "User created!", 201);
        }, (ex) => Response.Failure($"Error saving user: {ex.Message}", 500));
    }
}