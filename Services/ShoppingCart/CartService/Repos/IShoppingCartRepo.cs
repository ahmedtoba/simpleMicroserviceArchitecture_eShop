using CartService.Dtos;
using CartService.Models;

namespace CartService.Repos
{
    public interface IShoppingCartRepo
    {
        Task<Cart> GetCart(string username);
        Task<Cart> UpdateCart(Cart cart);
        Task ClearCart(string username);
        Task<bool> ProductExistsInCart(string username, CartItem product); 
        Task<Cart> AddProductToCart(string username, CartItem product);
    }
}