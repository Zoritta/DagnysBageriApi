using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DagnysBageriApi.Models.RequestModels
{
    public class UpdatePriceRequestModel
    {
        public int SupplierId { get; set; }
        public int ItemNumber { get; set; }
        public decimal NewPricePerKg { get; set; }
    }
}