namespace Inventario_API_REST.Extensions.Registers
{
    public static class RegisterCors
    {
        public static void AddCorsRegister(this IServiceCollection services)
        {
            services.AddCors(option =>
            {
                option.AddPolicy(CorsPolicy.InventaryAPIPolicy, conf =>
                {
                    conf.AllowAnyOrigin()
                     .AllowAnyHeader()
                     .AllowAnyMethod();
                });
            });
        }
    }
}
