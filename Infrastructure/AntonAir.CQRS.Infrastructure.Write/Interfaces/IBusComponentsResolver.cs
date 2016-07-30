using System.Collections.Generic;

namespace AntonAir.CQRS.Infrastructure.Write.Interfaces
{
	public interface IBusComponentsResolver
	{
		IEnumerable<TComponent> GetAll<TComponent>() where TComponent : IBusComponent;
	}
}