using EventBus.Messages.Events;
using MassTransit;
using StockService.API.Services;
using System.Threading.Tasks;

namespace StockService.API.Consumer
{
    public class PaymentRejectedEventConsumer
        : IConsumer<PaymentRejectedEvent>
    {
        private readonly IService _service;
        private readonly IPublishEndpoint _publishEndpoint;

        public PaymentRejectedEventConsumer(IService service, IPublishEndpoint publishEndpoint)
        {
            _service = service;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<PaymentRejectedEvent> context)
        {
            await _service.ReleaseStocksAsync(context.Message.OrderId);

            await _publishEndpoint.Publish(new StocksReleasedEvent
            {
                OrderId = context.Message.OrderId,
                Reason = context.Message.Reason
            });
        }
    }
}