using System.Diagnostics;

namespace Inventario_API_REST.Middlewares
{
    public class PerformanceMiddleware(RequestDelegate next, ILogger<PerformanceMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var timer = Stopwatch.StartNew();

            await next(context);

            timer.Stop();

            var totalTime = timer.ElapsedMilliseconds;
            var method = context.Request.Method.ToUpper();
            var path = context.Request.Path;

            if (totalTime > 500)
                logger.LogWarning($"⚠ SLOW ROUTE [{method}]: {path} [{totalTime}ms]");
            else logger.LogInformation($"[{method}]: {path} [{totalTime}ms]");
        }
    }
}
