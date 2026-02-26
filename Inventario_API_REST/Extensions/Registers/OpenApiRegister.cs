using Microsoft.OpenApi;

namespace Inventario_API_REST.Extensions.Registers
{
    public static class OpenApiRegister
    {
        public static void AddOpenApiDocRegister(this IServiceCollection services)
        {
            services.AddOpenApi(options => options.AddDocumentTransformer((doc, context, cancellToken) =>
            {
                //Info docs
                doc.Info.Title = "Inventory API";
                doc.Info.Version = "v1";
                var securityScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Insert your token here."
                };

                //Security Bearer Token Option
                doc.Components ??= new OpenApiComponents();
                doc.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
                doc.Components.SecuritySchemes["BearerAuth"] = securityScheme;

                var securityRequirement = new OpenApiSecurityRequirement();

                doc.Security = [securityRequirement];
                return Task.CompletedTask;
            }));
        }
    }
}
