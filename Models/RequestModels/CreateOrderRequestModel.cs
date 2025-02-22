
namespace DagnysBageriApi.Models.RequestModels
{
    public class CreateOrderRequestModel
    {
        public int CustomerId { get; set; }
        public List<OrderItemRequestModel> OrderItems { get; set; }
    }
    public class OrderItemRequestModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}