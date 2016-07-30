using System.Collections.Generic;

using AntonAir.CQRS.Infrastructure.Write.Interfaces;

using Microsoft.Practices.Unity;

namespace AntoAir.IoC
{
	public class UnityBusComponentsResolver : IBusComponentsResolver
	{
		private readonly IUnityContainer _container;

		public UnityBusComponentsResolver(IUnityContainer container)
		{
			this._container = container;
		}

		public IEnumerable<TComponent> GetAll<TComponent>() where TComponent : IBusComponent
		{
			var components = new List<TComponent>();

			if (this._container.IsRegistered<TComponent>())
			{
				var defaultComponent = this._container.Resolve<TComponent>();
				components.Add(defaultComponent);
			}

			var resolvedComponents = this._container.ResolveAll<TComponent>();
			components.AddRange(resolvedComponents);

			return components;
		}
	}
}