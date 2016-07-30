using AntonAir.CQRS.Infrastructure.Write.Interfaces.Events;

namespace AntonAir.CQRS.Infrastructure.Write.Core
{
	public abstract class BaseCommandHandler
	{
		protected readonly IEventBus _eventBus;

		public BaseCommandHandler(IEventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		protected void RaiseEvent<TEvent>(TEvent @event) where TEvent : class, IEvent, new()
		{
			this._eventBus.Publish(@event);
		}
	}
}