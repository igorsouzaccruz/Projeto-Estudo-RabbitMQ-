using MassTransit;

namespace ProjetoRabbitMQ.Bus
{
    internal interface IPublishBus
    {
        Task PublishAsync<T>(T message, CancellationToken ct = default) where T : class;
    }

    internal class PublishBus : IPublishBus
    {
        private readonly IPublishEndpoint _busEndPoint;

        public PublishBus(IPublishEndpoint publish)
        {
            _busEndPoint = publish;
        }

        public Task PublishAsync<T>(T message, CancellationToken ct = default) where T : class
        {
            return _busEndPoint.Publish(message, ct);
        }
    }
}
