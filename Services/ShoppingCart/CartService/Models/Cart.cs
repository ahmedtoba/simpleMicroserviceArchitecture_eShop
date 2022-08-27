namespace CartService.Models
{
    public class Cart
    {
        public string? Username { get; set; }
        public List<CartItem> CartItems { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }
        public Cart(string username)
        {
            Username = username;
            CartItems = new List<CartItem>();
        }

        public decimal TotalPrice
        {
            get
            {
                return CartItems.Sum(x => x.Price * x.Quantity);
            }
        }
    }
}