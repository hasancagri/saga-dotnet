using OrderService.API.Models;
using System.Threading.Tasks;

namespace OrderService.API.Services
{
    public interface IService
    {
        Task CreateOrderAsync(CreateOrderRequest request);
        Task CompleteOrderAsync(int orderId);
        Task RejectOrderAsync(int orderId, string reason);
    }
}
