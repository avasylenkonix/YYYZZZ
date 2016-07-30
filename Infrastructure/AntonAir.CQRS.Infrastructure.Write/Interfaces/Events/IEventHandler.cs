namespace AntonAir.CQRS.Infrastructure.Write.Interfaces.Events
{
	public interface IEventHandler { }

	public interface IEventHandler<TEvent> : IEventHandler, IBusComponent where TEvent : class, IEvent
	{
		void Handle(TEvent eventMessage);
	}

	public interface IAsyncEventHandler<TEvent> : IEventHandler, IBusComponent where TEvent : class, IEvent
	{
		void Handle(TEvent eventMessage);
	}
}