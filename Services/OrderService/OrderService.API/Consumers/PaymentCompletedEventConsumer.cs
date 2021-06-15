using System.Diagnostics;
using System.Threading.Tasks;
using EventBus.Messages.Events;
using MassTransit;

namespace OrderService.API.Consumers
{
    public class PaymentCompletedEventConsumer
        : IConsumer<PaymentCompletedEvent>
    {
        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            Debug.WriteLine("PaymentCompletedEvent");
        }
    }
}