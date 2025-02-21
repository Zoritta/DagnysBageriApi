

namespace DagnysBageriApi.Models.RequestModels
{
    public class AddCustomerRequestModel
    {
        public string StoreName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string DeliveryAddress { get; set; }
        public string InvoiceAddress { get; set; }
    }
}