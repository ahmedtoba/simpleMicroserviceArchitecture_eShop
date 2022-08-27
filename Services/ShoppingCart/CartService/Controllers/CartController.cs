using AutoMapper;
using CartService.Repos;
using CartService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers
{
    [ApiController]
    [Route("api/c/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IShoppingCartRepo _cartRepo;
        public CartController (IShoppingCartRepo cartRepo)
        {
            _cartRepo = cartRepo;
        }

        [HttpGet("{username}", Name = "GetCart")]
        public async Task<ActionResult<Cart>> GetCart(string username)
        {
            var cart = await _cartRepo.GetCart(username);
            return Ok(cart == null ? new Cart(username) : cart);
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> UpdateCart(Cart cart)
        {
            return Ok(await _cartRepo.UpdateCart(cart));
        }

        [HttpDelete("{username}", Name = "ClearCart")]
        public async Task<ActionResult> DeleteCart(string username)
        {
            await _cartRepo.ClearCart(username);
            return Ok();
        }

    }
}