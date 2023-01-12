using EventBus.Events;

namespace DataTransfer;

public record DatabaseUpdate() : EventRecord("DATABASE_UPDATE");