using System.Threading.Tasks;

namespace StockService.API.Services
{
    public interface IService
    {
        Task ReserveStocksAsync(int orderId);
        Task<bool> ReleaseStocksAsync(int orderId);
    }
}
