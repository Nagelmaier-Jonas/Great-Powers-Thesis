using EventBus.Events;

namespace DataTransfer;

public record ReadyEvent() : EventRecord("READY_EVENT");