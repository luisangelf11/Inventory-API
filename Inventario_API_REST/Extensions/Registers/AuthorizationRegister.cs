using System.Reflection;

namespace Inventario_API_REST.Extensions.Registers
{
    public static class AuthorizationRegister
    {
        public static void AddAuthorizationAndPolicy(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy(RolesName.Admin, policy => policy.RequireRole(RolesName.Admin))
                .RegisterDynamicPermissions();
        }

        private static AuthorizationBuilder RegisterDynamicPermissions(this AuthorizationBuilder builder)
        {
            var permissions = typeof(Permissions)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string));

            foreach (var field in permissions)
            {
                var permissionValue = field.GetValue(null)?.ToString();

                if (!string.IsNullOrEmpty(permissionValue))
                    builder.AddPolicy(permissionValue, policy =>
                        policy.RequireClaim("permission", permissionValue));
            }

            return builder;
        }
    }
}
