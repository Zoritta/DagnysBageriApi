using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DagnysBageriApi.Models.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
        public CustomerViewModel Customer { get; set; }

    }
}