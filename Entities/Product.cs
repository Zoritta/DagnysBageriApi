namespace DagnysBageriApi.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal PricePerUnit { get; set; }
        public double Weight { get; set; } 
        public int QuantityPerPack { get; set; } 
        public DateTime BestBeforeDate { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public IList<Order> Orders { get; set; } = new List<Order>(); 
    }
}
