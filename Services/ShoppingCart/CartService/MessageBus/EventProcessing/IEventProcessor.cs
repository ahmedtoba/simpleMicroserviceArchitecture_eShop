namespace CartService.MessageBus.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}