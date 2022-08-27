using CartService.Models;

namespace CartService.Repos
{
    public interface IShoppingCartRepo
    {
        Task<Cart> GetCart(string username);
        Task<Cart> UpdateCart(Cart cart);
        Task ClearCart(string username);
    }
}