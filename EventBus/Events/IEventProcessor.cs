namespace EventBus.Events; 

public interface IEventProcessor {
    void ProcessEvent(string eventMessage);
}