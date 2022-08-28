using CartService.Dtos;
using CartService.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CartService.Repos
{
    public class ShoppingCartRepo : IShoppingCartRepo
    {
        private readonly IDistributedCache _cache;

        public ShoppingCartRepo(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<Cart> GetCart(string username)
        {
            var cart = await _cache.GetStringAsync(username);
            if (string.IsNullOrEmpty(cart))
                return null;
            return JsonConvert.DeserializeObject<Cart>(cart);   
        }

        public async Task<Cart> UpdateCart(Cart cart)
        {
            var cartString = JsonConvert.SerializeObject(cart);
            await _cache.SetStringAsync(cart.Username, cartString);
            return await GetCart(cart.Username);
        }
        public Task ClearCart(string username)
        {
            return _cache.RemoveAsync(username);
        }

        public async Task<bool> ProductExistsInCart(string username, CartItem product)
        {
            var cart = await GetCart(username);
            if (cart == null)
                return false;
            return cart.CartItems.Any(x => x.ProductId == product.ProductId);
        }
        public async Task<Cart> AddProductToCart(string username, CartItem product)
        {
            var cart = await GetCart(username);
            if (cart == null)
                cart = new Cart { Username = username };
            
            if (await ProductExistsInCart(username, product))
                cart.CartItems.First(i => i.ProductId == product.ProductId).Quantity++;
            else
                cart.CartItems.Add(product);
                
            return await UpdateCart(cart);
        }
    }
}