using Microsoft.AspNetCore.Mvc;
using CartService.Controllers;
using CartService.Models;
using CartService.Repos;
using Moq;

namespace Cart_UnitTest
{
    public class CartControllerTest
    {
        private readonly Mock<IShoppingCartRepo> _cartRepoMock;

        public CartControllerTest()
        {
            _cartRepoMock = new ();
        }

        [Fact]
        public async Task GetUserCart_GivenUsernam_ReturnsUserCart()
        {
            //Arrange
            var username = "Ahmed";
            var cart = new Cart(username);
            _cartRepoMock.Setup(x => x.GetCart(username)).ReturnsAsync(cart);
            var cartController = new CartController(_cartRepoMock.Object);

            //Act
            var actionResult = await cartController.GetCart(username);
            
            //Assert
            var result = actionResult.Result as OkObjectResult;
            Assert.NotNull(result);

            var actualCart = Assert.IsAssignableFrom<Cart>(result.Value);

            Assert.True(actualCart.Username == username);
        }

        [Fact]
        public async Task UpdateCart_GivenCart_ReturnsUpdatedCart()
        {
            // Arrange
            var oldCart = new Cart("fathy");

            var newCart = new Cart("fathy");
            newCart.CartItems.Add(new CartItem()
            {
                Quantity = 1,
                ProductId = 1,
                Price = 500
            });
            _cartRepoMock.Setup(x => x.UpdateCart(oldCart)).ReturnsAsync(newCart);

            var cartController = new CartController(_cartRepoMock.Object);

            // Act
            
            var actionResult = await cartController.UpdateCart(oldCart);

            // Assert

            Assert.IsType<ActionResult<Cart>>(actionResult);

            var result = actionResult.Result as OkObjectResult;

            Assert.NotNull(result);

            var actualCart = Assert.IsAssignableFrom<Cart>(result.Value);

            Assert.True(newCart.Username == actualCart.Username);

            Assert.True(newCart.CartItems.Count == 1);
        }

    }
}