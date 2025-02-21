using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DagnysBageriApi.Models.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string DeliveryAddress { get; set; }
        public string InvoiceAddress { get; set; }
        public List<OrderViewModel> Orders { get; set; }
    }
}