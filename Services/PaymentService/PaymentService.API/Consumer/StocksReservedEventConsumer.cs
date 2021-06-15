using EventBus.Messages.Events;
using MassTransit;
using PaymentService.API.Services;
using System.Threading.Tasks;

namespace PaymentService.API.Consumer
{
    public class StocksReservedEventConsumer
        : IConsumer<StocksReservedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IService _service;

        public StocksReservedEventConsumer(IService service, IPublishEndpoint publishEndpoint)
        {
            _service = service;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<StocksReservedEvent> context)
        {
            var result = _service.DoPaymentAsync();

            if (result)
            {
                await _publishEndpoint.Publish(new PaymentCompletedEvent
                {
                    OrderId = 1
                });
            }
            else
            {
                await _publishEndpoint.Publish(new PaymentRejectedEvent
                {
                    OrderId = 1,
                    Reason = "İşlem Başarısız"
                });
            }
        }
    }
}
