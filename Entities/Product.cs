namespace DagnysBageriApi.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public int QuantityPerPack { get; set; }
        public DateTime BestBeforeDate { get; set; }
        public DateTime ManufactureDate { get; set; }

        public string? ImageUrl { get; set; }

        internal bool Any()
        {
            throw new NotImplementedException();
        }

        public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}