using ESourcing.Order.Consumers;
using Microsoft.AspNetCore.Builder;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ESourcing.Order.Extensions
{
    //Çalışma süresince dataları consume etmek için bir extension class
    public static class ApplicationBuilderExtensions
    {
        //consumer örneği ekliyoruz
        public static EventBusOrderCreateConsumer Listener { get; set; }

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<EventBusOrderCreateConsumer>();

            //middleware in lifecycle ını belirliyoruz
            var lifeCycle = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            //proje çalıştığı sürece ne yapacağını belirtiyoruz
            lifeCycle.ApplicationStarted.Register(OnStarted);
            lifeCycle.ApplicationStopping.Register(OnStopping);
            return app;
        }

        private static void OnStarted()
        {
            Listener.Consume();
        }

        private static void OnStopping()
        {
            Listener.Disconnect();
        }
    }
}
