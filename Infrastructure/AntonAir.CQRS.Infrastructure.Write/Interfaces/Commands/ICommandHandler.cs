namespace AntonAir.CQRS.Infrastructure.Write.Interfaces.Commands
{
	public interface ICommandHandler { }

	public interface ICommandHandler<TCommand> : ICommandHandler, IBusComponent where TCommand : class, ICommand
	{
		void Handle(TCommand command);
	}
}