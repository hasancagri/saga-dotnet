using EventBus.Messages.Events;
using MassTransit;
using StockService.API.Services;
using System;
using System.Threading.Tasks;

namespace StockService.API.Consumer
{
    public class PaymentCompletedEventConsumer
        : IConsumer<PaymentCompletedEvent>
    {
        private readonly IService _service;
        private readonly IPublishEndpoint _publishEndpoint;

        public PaymentCompletedEventConsumer(IService service, IPublishEndpoint publishEndpoint)
        {
            _service = service;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            await _service.ReserveStocksAsync(context.Message.OrderId);
            await _publishEndpoint.Publish(new StocksReservedEvent
            {
                OrderId = context.Message.OrderId,
            });
        }
    }
}
