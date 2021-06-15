using EventBus.Messages.Events;
using MassTransit;
using StockService.API.Services;
using System.Threading.Tasks;

namespace StockService.API.Consumer
{
    public class OrderCreatedEventConsumer
        : IConsumer<OrderCreatedEvent>
    {
        private readonly IService _service;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderCreatedEventConsumer(IService service, IPublishEndpoint publishEndpoint)
        {
            _service = service;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            await _service.ReserveStocksAsync(context.Message.OrderId);
            await _publishEndpoint.Publish(new StocksReservedEvent
            {
                UserId = context.Message.UserId,
                OrderId = context.Message.OrderId,
                WalletId = context.Message.WalletId,
                TotalAmount = context.Message.TotalAmount
            });
        }
    }
}
