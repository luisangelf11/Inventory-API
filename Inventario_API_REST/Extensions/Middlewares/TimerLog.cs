using Inventario_API_REST.Middlewares;

namespace Inventario_API_REST.Extensions.Middlewares
{
    public static class TimerLog
    {
        public static IApplicationBuilder UseLogTime(this IApplicationBuilder app) =>
            app.UseMiddleware<PerformanceMiddleware>();
    }
}
