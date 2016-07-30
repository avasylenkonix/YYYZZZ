using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;

using AntonAir.App_Start;
using AntonAir.Controllers;
using AntonAir.CQRS.Infrastructure.IoC;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;

namespace AntonAir.IoC
{
	public static class Bootstrapper
	{
		public static void Bootstrap()
		{
			var container = UnityConfig.GetConfiguredContainer();

			var referencedTypes = AllClasses.FromAssemblies(
				BuildManager.GetReferencedAssemblies().Cast<Assembly>());

			container.RegisterTypes(
					 referencedTypes
						 .Where(type => typeof(IModuleBootstrapper<IUnityContainer>).IsAssignableFrom(type)),
					 WithMappings.FromAllInterfaces,
					 WithName.TypeName,
					 WithLifetime.Transient);

			var bootstrappers = container.ResolveAll<IModuleBootstrapper<IUnityContainer>>();

			foreach (var bootstrapper in bootstrappers)
			{
				bootstrapper.Bootstrap(container, referencedTypes);
			}
			
			DependencyResolver.SetResolver(new UnityDependencyResolver(container));
		}
	}
}