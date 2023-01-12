using EventBus.Events;

namespace DataTransfer;

public record StateHasChangedEvent() : EventRecord("STATE_HAS_CHANGED");