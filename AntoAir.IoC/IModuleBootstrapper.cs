using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonAir.CQRS.Infrastructure.IoC
{
	public interface IModuleBootstrapper<TContainer>
	{
		void Bootstrap(TContainer container, IEnumerable<Type> referencedTypes);
	}
}
