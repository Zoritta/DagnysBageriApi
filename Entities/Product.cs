namespace DagnysBageriApi.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public int QuantityPerPack { get; set; }
        public DateTime BestBeforeDate { get; set; }
        public DateTime ManufactureDate { get; set; }
        public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); 
    }
}
