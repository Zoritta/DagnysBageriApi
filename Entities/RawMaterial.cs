

namespace DagnysBageriApi.Entities
{
    public class RawMaterial
    {
        public int RawMaterialId { get; set; }
        public string ItemNumber { get; set; }
        public string Name { get; set; }
        public IList<SupplierMaterial> SupplierMaterials { get; set; }

    }
}