

namespace DagnysBageriApi.Models.ResponseModels
{
    public class OrderResponseModel
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string StoreName { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public List<OrderItemResponse> OrderItems { get; set; }
        public decimal TotalOrderPrice { get; set; }
    }
    public class OrderItemResponse
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}