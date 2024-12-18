

namespace DagnysBageriApi.Models.RequestModels
{
    public class AddMaterialToSupplierRequest
    {
        public string SupplierName { get; set; }
        public string ItemNumber { get; set; }
        public string Name { get; set; }
        public decimal PricePerKg { get; set; }
    }
}