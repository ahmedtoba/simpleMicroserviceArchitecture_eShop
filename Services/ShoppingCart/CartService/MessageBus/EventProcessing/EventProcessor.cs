using System.Text.Json;
using AutoMapper;
using CartService.Dtos;
using CartService.Models;
using CartService.Repos;

namespace CartService.MessageBus.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, AutoMapper.IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.ProductAddedToCart:
                    addPlatform(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notifcationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GeneralEventDto>(notifcationMessage);

            switch(eventType.Event)
            {
                case "Platform_Published":
                    Console.WriteLine("--> Platform Published Event Detected");
                    return EventType.ProductAddedToCart;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void addPlatform(string productAddedToCartMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IShoppingCartRepo>();
                
                var productPublishDto = JsonSerializer.Deserialize<ProductPublishDto>(productAddedToCartMessage);

                try
                {
                    var cartItem = _mapper.Map<CartItem>(productPublishDto);
                    var username = productPublishDto.Username;
                    repo.AddProductToCart(username, cartItem);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Platform to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        ProductAddedToCart,
        Undetermined
    }
}