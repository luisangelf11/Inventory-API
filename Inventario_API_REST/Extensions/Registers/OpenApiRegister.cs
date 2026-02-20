namespace Inventario_API_REST.Extensions.Registers
{
    public static class OpenApiRegister
    {
        public static void AddOpenApiDocRegister(this IServiceCollection services) {
            services.AddOpenApi(options => options.AddDocumentTransformer((doc, context, cancellToken) =>
            {
                doc.Info.Title = "Inventory API";
                doc.Info.Version = "v1";
                return Task.CompletedTask;
            }));
        }
    }
}
