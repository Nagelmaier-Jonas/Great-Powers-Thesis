﻿namespace EventBus.Clients; 

public interface IEventPublisher {
    
    void Publish(string message);
}