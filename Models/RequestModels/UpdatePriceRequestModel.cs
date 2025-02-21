

namespace DagnysBageriApi.Models.RequestModels
{
    public class UpdatePriceRequestModel
    {
        public int SupplierId { get; set; }
        public string ItemNumber { get; set; }
        public decimal NewPricePerKg { get; set; }
    }
}