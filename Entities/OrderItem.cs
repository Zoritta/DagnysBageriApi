namespace DagnysBageriApi.Entities
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal Price{ get; set; }

        public decimal TotalPrice => Quantity * Price;
    }
}
