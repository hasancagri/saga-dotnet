using System.Threading.Tasks;

namespace StockService.API.Services
{
    public class Service 
        : IService
    {
        public Task<bool> ReleaseStocksAsync(int orderId)
        {
            // Stock release logic

            return Task.FromResult(true);
        }

        public Task ReserveStocksAsync(int orderId)
        {
            // Stock reserve logic

            return Task.CompletedTask;
        }
    }
}
