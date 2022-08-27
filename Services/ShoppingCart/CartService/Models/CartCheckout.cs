namespace CartService.Models
{
    public class CartCheckout
    {
        public string? Username { get; set; }
        public decimal TotalPrice { get; set; }
        
        // user shipping data
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? ZipCode { get; set; }

        // payment data
        public string? CardName { get; set; }
        public string? CardNumber { get; set; }
        public string? Expiration { get; set; }
        public string? CVV { get; set; }
        public int? PaymentMethod { get; set; }
    }
}