namespace DagnysBageriApi.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; } // Unique identifier
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // Order details
        public decimal TotalPrice => OrderItems.Sum(item => item.TotalPrice);
    }
}
