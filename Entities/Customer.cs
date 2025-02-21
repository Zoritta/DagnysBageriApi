namespace DagnysBageriApi.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string StoreName { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string DeliveryAddress { get; set; }
        public string InvoiceAddress { get; set; }
        public IList<Order> Orders { get; set; } = new List<Order>();
    }
}