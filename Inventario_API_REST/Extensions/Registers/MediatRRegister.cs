using Inventario_API_REST.Share.MyMediatR;
using System.Reflection;

namespace Inventario_API_REST.Extensions.Registers
{
    public static class MediatRRegister
    {
        public static void AddMediatR(this IServiceCollection services, Assembly assembly)
        {
            var handlerInterface = typeof(IHandler<,>);

            var handlers = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract).Select(t => new
            {

                Implementation = t,
                Interfaces = t.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
            }).Where(x => x.Interfaces.Any());

            foreach (var handler in handlers) { 
                foreach(var @inter in handler.Interfaces)
                {
                    services.AddScoped(@inter, handler.Implementation);
                }
            }

            services.AddScoped<IMediatR, MediatR>();
        }
    }
}
