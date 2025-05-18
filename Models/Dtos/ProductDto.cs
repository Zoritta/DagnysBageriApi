using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DagnysBageriApi.Models.Dtos
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public int QuantityPerPack { get; set; }
        public DateTime BestBeforeDate { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string? ImageUrl { get; set; }
    }
}