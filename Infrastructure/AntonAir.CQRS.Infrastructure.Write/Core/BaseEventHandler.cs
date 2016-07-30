using AntonAir.CQRS.Infrastructure.Write.Interfaces.Commands;

namespace AntonAir.CQRS.Infrastructure.Write.Core
{
	public abstract class BaseEventHandler
	{
		protected readonly ICommandBus _commandBus;

		protected BaseEventHandler(ICommandBus commandBus)
		{
			this._commandBus = commandBus;
		}

		protected void PublishCommand<TCommand>(TCommand command) where TCommand : class, ICommand, new()
		{
			this._commandBus.Publish(command);
		}
	}
}