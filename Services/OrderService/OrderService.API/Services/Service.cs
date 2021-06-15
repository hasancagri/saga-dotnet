using EventBus.Messages.Events;
using MassTransit;
using OrderService.API.Models;
using System.Threading.Tasks;

namespace OrderService.API.Services
{
    public class Service
        : IService
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public Service(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task CreateOrderAsync(CreateOrderRequest request)
        {
            // Order creation logic in "Pending" state.

            await _publishEndpoint.Publish(new OrderCreatedEvent
            {
                UserId = 1,
                OrderId = 1,
                WalletId = 1,
                TotalAmount = request.TotalAmount,
            });
        }

        public Task CompleteOrderAsync(int orderId)
        {
            // Change the order status as completed.
            return Task.CompletedTask;
        }

        public Task RejectOrderAsync(int orderId, string reason)
        {
            // Change the order status as rejected.
            return Task.CompletedTask;
        }
    }
}
