﻿namespace EventBus.Events; 

public interface IEventHandler {
    void Execute();
}