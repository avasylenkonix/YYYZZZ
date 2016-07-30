namespace AntonAir.CQRS.Infrastructure.Write.Interfaces.Commands
{
	public interface ICommandBus
	{
		void Publish<TCommand>(TCommand command) where TCommand : class, ICommand, new();

		void Publish(ICommand command);
	}
}