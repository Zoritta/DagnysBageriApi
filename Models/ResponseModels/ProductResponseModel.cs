

namespace DagnysBageriApi.Models.ResponseModels
{
    public class ProductResponseModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}