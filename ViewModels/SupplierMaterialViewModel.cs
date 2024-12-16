using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DagnysBageriApi.Entities;

namespace DagnysBageriApi.ViewModels
{
    public class SupplierMaterialViewModel
    {
        public decimal PricePerKg { get; set; }
        public SupplierViewModel Supplier { get; set; }
    }
}