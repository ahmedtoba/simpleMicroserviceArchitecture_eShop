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
    }
}