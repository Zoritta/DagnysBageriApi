

namespace DagnysBageriApi.Models.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Weight { get; set; }
        public int QuantityPerPackage { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime ManufacturingDate { get; set; }
    }
}