namespace AntonAir.CQRS.Infrastructure.Write.Interfaces.Commands
{
	public interface ICommandValidator<TCommand> : IBusComponent where TCommand : ICommand
	{
		void Validate(TCommand command);

		int Order { get; }
	}
}