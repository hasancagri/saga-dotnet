using System.Diagnostics;
using System.Threading.Tasks;
using EventBus.Messages.Events;
using MassTransit;

namespace OrderService.API.Consumers
{
    public class StocksReleasedEventConsumer
        : IConsumer<StocksReleasedEvent>
    {
        public async Task Consume(ConsumeContext<StocksReleasedEvent> context)
        {
            Debug.WriteLine("StocksReleasedEvent");
        }
    }
}