using Inventario_API_REST.Middlewares;

namespace Inventario_API_REST.Extensions.Middlewares
{
    public static class GlobalException
    {
        public static IApplicationBuilder UseGlobalException(this IApplicationBuilder app) =>
             app.UseMiddleware<ExceptionMiddleware>();
    }
}
