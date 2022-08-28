using CatalogService.Dtos;

namespace CatalogService.MessageBus
{
    public interface IMessageBusClient
    {
        void AddProductToCart(ProductPublishDto product);
    }
}