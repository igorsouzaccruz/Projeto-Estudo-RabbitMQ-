using MassTransit;
using ProjetoRabbitMQ.Bus;

namespace ProjetoRabbitMQ.Extensions
{
    public static class AppExtensions
    {
        public static void AddRabbitMQService(this IServiceCollection services) 
        {
            services.AddTransient<IPublishBus, PublishBus>();

            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddConsumer<RelatorioSolicitadoEventConsumer>();

                busConfigurator.UsingRabbitMq((ctx,cfg) => 
                {
                    cfg.Host(new Uri("amqp://localhost:5672"), host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    cfg.ConfigureEndpoints(ctx);
                });
            });
        }
    }
}
