
namespace DagnysBageriApi.Entities
{
    public class SupplierMaterial
    {
        public int SupplierId { get; set; }
        public int RawMaterialId { get; set; }
        public decimal PricePerKg { get; set; }
        public Supplier Supplier { get; set; }
        public RawMaterial RawMaterial { get; set; }
    }
}