
namespace DagnysBageriApi.ViewModels
{
    public class RawMaterialViewModel
    {
        public string ItemNumber { get; set; }
        public string Name { get; set; }
        public IList<SupplierMaterialViewModel> Suppliers { get; set; }
    }
}