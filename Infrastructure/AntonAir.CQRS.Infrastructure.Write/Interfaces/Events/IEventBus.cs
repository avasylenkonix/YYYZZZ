namespace AntonAir.CQRS.Infrastructure.Write.Interfaces.Events
{
	public interface IEventBus
	{
		void Publish<TEvent>(TEvent eventMessage) where TEvent : class, IEvent, new();
	}
}