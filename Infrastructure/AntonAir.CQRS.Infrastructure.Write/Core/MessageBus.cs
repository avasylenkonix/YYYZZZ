using AntonAir.CQRS.Infrastructure.Write.Interfaces;
using AntonAir.CQRS.Infrastructure.Write.Interfaces.Commands;
using AntonAir.CQRS.Infrastructure.Write.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AntonAir.CQRS.Infrastructure.Write.Core
{
	/// <summary>
	///
	/// </summary>
	/// <seealso cref="ICommandBus" />
	/// <seealso cref="IEventBus" />
	public class MessageBus : ICommandBus, IEventBus
	{
		/// <summary>
		/// The _resolver
		/// </summary>
		private readonly IBusComponentsResolver resolver;

		/// <summary>
		/// Initializes a new instance of the <see cref="MessageBus"/> class.
		/// </summary>
		/// <param name="resolver">The resolver.</param>
		public MessageBus(IBusComponentsResolver resolver)
		{
			this.resolver = resolver;
		}

		/// <summary>
		/// Publishes the specified command.
		/// </summary>
		/// <typeparam name="TCommand">The type of the command.</typeparam>
		/// <param name="command">The command.</param>
		/// <exception cref="System.InvalidOperationException"></exception>
		void ICommandBus.Publish<TCommand>(TCommand command)
		{
			var validators = this.resolver.GetAll<ICommandValidator<TCommand>>();
			if (validators != null && validators.Any())
			{
				foreach (var validator in validators)
				{
					validator.Validate(command);
				}
			}

			var handlers = this.resolver.GetAll<ICommandHandler<TCommand>>();
			if (handlers == null || handlers.Count() == 0)
				throw new InvalidOperationException(string.Format("Command handler for {0} cannot be found.", command.GetType().Name));

			handlers.Single().Handle(command);
		}

		/// <summary>
		/// Publishes the specified command.
		/// </summary>
		/// <param name="command">The command.</param>
		void ICommandBus.Publish(ICommand command)
		{
			var publishMethod = typeof(ICommandBus).GetMethods().First(m => m.Name == "Publish" && m.IsGenericMethod).MakeGenericMethod(command.GetType());
			publishMethod.Invoke(this, new object[] { command });
		}

		/// <summary>
		/// Publishes the specified event message.
		/// </summary>
		/// <typeparam name="TEvent">The type of the event.</typeparam>
		/// <param name="eventMessage">The event message.</param>
		/// <exception cref="AggregateException"></exception>
		void IEventBus.Publish<TEvent>(TEvent eventMessage)
		{
			var handlers = this.resolver.GetAll<IEventHandler<TEvent>>();

			if (handlers != null && handlers.Any())
			{
				var exceptions = new Queue<Exception>();
				foreach (var handler in handlers)
				{
					try
					{
						handler.Handle(eventMessage);
					}
					catch (Exception exception)
					{
						exceptions.Enqueue(exception);
					}
				}

				if (exceptions.Any())
				{
					if (exceptions.Count == 1)
					{
						throw exceptions.First();
					}

					throw new AggregateException(exceptions);
				}
			}
		}
	}
}