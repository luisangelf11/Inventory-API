using Inventario_API_REST.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Inventario_API_REST.Features.Auth;

using Response = Result<LoginResponse>;

public record AuthLoginCommand(string Username, string Password) : IRequest<Response>;
public record LoginResponse(string Token);
public class LoginHandler(InventoryDbContext dbContext, IConfiguration config) : IHandler<AuthLoginCommand, Response>
{
    public async Task<Response> Handle(AuthLoginCommand request, CancellationToken cancellationToken = default)
    {
        return await AsyncHandler.TryCatchAsync(async () =>
        {
            // Find user and include role
            var user = await dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

            // Validation
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return Response.Failure("Invalid username or password.", 401);


            // Generate Claims
            var claims = new List<Claim> {
                        new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new(ClaimTypes.Name, user.Username),
                        new(ClaimTypes.Role, user.Role.Name),
                        };

            // Add permissions
            if (!string.IsNullOrEmpty(user.Permissions))
            {
                var permissions = user.Permissions.Split(';', StringSplitOptions.RemoveEmptyEntries);
                foreach (var p in permissions)
                {
                    claims.Add(new Claim("permission", p.Trim()));
                }
            }

            // Generate JWT
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds
            );

            var jwtString = new JwtSecurityTokenHandler().WriteToken(token);

            // Return Token
            return Response.Ok(new LoginResponse(jwtString));
        }, (ex) => Response.Failure($"Error Login user: {ex.Message}", 500));
    }
}