using Microsoft.AspNetCore.Mvc;
using OrderService.API.Models;
using OrderService.API.Services;
using System.Net;
using System.Threading.Tasks;

namespace OrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController
        : ControllerBase
    {
        private readonly IService _service;
        public OrdersController(IService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            await _service.CreateOrderAsync(request);
            return Accepted();
        }
    }
}
