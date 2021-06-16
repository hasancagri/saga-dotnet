using System.Collections.Generic;

namespace OrderService.API.Models
{
    public class CreateOrderRequest
    {
        public int UserId { get; set; }
        public List<OrderItemRequest> OrderItems { get; set; } = new List<OrderItemRequest>();
        public int WalletId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
