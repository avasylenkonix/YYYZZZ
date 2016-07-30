using System;
using System.Collections.Generic;
using System.Linq;

using AntoAir.IoC;

using AntonAir.CQRS.Infrastructure.Read.Core.Repositories.Filters;
using AntonAir.CQRS.Infrastructure.Read.Core.Services.List;
using AntonAir.CQRS.Infrastructure.Read.Core.Services.Single;
using AntonAir.CQRS.Infrastructure.Read.Interfaces;
using AntonAir.CQRS.Infrastructure.Read.Interfaces.Repositories;
using AntonAir.CQRS.Infrastructure.Read.Interfaces.Repositories.Filters;
using AntonAir.CQRS.Infrastructure.Read.Interfaces.Services.List;
using AntonAir.CQRS.Infrastructure.Read.Interfaces.Services.Single;
using AntonAir.CQRS.Infrastructure.Write.Core;
using AntonAir.CQRS.Infrastructure.Write.Interfaces;
using AntonAir.CQRS.Infrastructure.Write.Interfaces.Commands;
using AntonAir.CQRS.Infrastructure.Write.Interfaces.Events;
using AntonAir.Data.Contexts;
using AntonAir.DataAccess.Repository.Core;
using AntonAir.DataAccess.Repository.Interfaces;
using AntonAir.Domain;
using AntonAir.DomainObjects.SearchCriteria;
using AntonAir.DomainObjects.ViewModel;

using Microsoft.Practices.Unity;

namespace AntonAir.CQRS.Infrastructure.IoC
{
	public class Bootstrapper : IModuleBootstrapper<IUnityContainer>
	{
		public void Bootstrap(IUnityContainer container, IEnumerable<Type> referencedTypes)
		{
			container.RegisterType<IUnitOfWork, UnitOfWork>();
			container.RegisterType<AntonAirDbContext>(new InjectionFactory(c => new AntonAirDbContext()));

			container.RegisterType<IBusComponentsResolver, UnityBusComponentsResolver>(new HierarchicalLifetimeManager(), new InjectionFactory(x => new UnityBusComponentsResolver(x)));
			container.RegisterType<IEventBus, MessageBus>(new HierarchicalLifetimeManager());
			container.RegisterType<ICommandBus, MessageBus>(new HierarchicalLifetimeManager());

			container.RegisterType<IPipeLineFactory, PipeLineFactory>();
			//register default instances
			container.RegisterTypes(
					referencedTypes
						.Where(type =>
								 type.GetInterfaces().Any(t =>
										 t.IsGenericType && !t.ContainsGenericParameters
										 && (
											typeof(DataAccess.Repository.Interfaces.IReadRepository<>).IsAssignableFrom(t.GetGenericTypeDefinition())
											|| typeof(IReadRepository<,>).IsAssignableFrom(t.GetGenericTypeDefinition())
											|| typeof(IReadListModelRepository<,>).IsAssignableFrom(t.GetGenericTypeDefinition())
											|| typeof(IViewModelFactory<,>).IsAssignableFrom(t.GetGenericTypeDefinition())
											|| typeof(IViewModelFactory<>).IsAssignableFrom(t.GetGenericTypeDefinition())
											|| typeof(ISingleViewModelService<,,>).IsAssignableFrom(t.GetGenericTypeDefinition())
											|| typeof(ISingleViewModelService<,>).IsAssignableFrom(t.GetGenericTypeDefinition())
											|| typeof(ISingleViewModelService<>).IsAssignableFrom(t.GetGenericTypeDefinition())
											|| typeof(IListViewModelService<,>).IsAssignableFrom(t.GetGenericTypeDefinition())
											|| typeof(IAsyncListViewModelService<,>).IsAssignableFrom(t.GetGenericTypeDefinition())
											|| typeof(IListViewModelService<>).IsAssignableFrom(t.GetGenericTypeDefinition())
											|| typeof(IAsyncListViewModelService<>).IsAssignableFrom(t.GetGenericTypeDefinition())
										 ))),
					WithMappings.FromAllInterfaces,
					WithName.Default,
					WithLifetime.Hierarchical);

			container.RegisterTypes(
					referencedTypes
						.Where(type => typeof(IListViewModelService<,>).IsAssignableFrom(type)
									   || typeof(IAsyncListViewModelService<,>).IsAssignableFrom(type)
									   || typeof(IAsyncListViewModelService<>).IsAssignableFrom(type)
									   || typeof(IListViewModelService<>).IsAssignableFrom(type)),
					WithMappings.FromAllInterfaces,
					WithName.Default,
					WithLifetime.Hierarchical);

			//register named instances
			container.RegisterTypes(
					referencedTypes
						.Where(type =>
								 type.GetInterfaces().Any(t =>
										 t.IsGenericType && !t.ContainsGenericParameters
										 && (
										 typeof(ICommandHandler<>).IsAssignableFrom(t.GetGenericTypeDefinition())
										 || typeof(IEventHandler<>).IsAssignableFrom(t.GetGenericTypeDefinition())
										 || typeof(ICommandValidator<>).IsAssignableFrom(t.GetGenericTypeDefinition())
										 || typeof(IQueryFilter<,>).IsAssignableFrom(t.GetGenericTypeDefinition())))),
					WithMappings.FromAllInterfaces,
					WithName.TypeName,
					WithLifetime.Hierarchical);

			container.RegisterType(typeof(ISingleViewModelService<,,>), new InjectionFactory((c, t, s) =>
			{
				var readSingleModelRepositoryType = typeof(IReadRepository<,>).MakeGenericType(t.GenericTypeArguments[0], t.GenericTypeArguments[1]);

				var viewModelFactoryType = typeof(IViewModelFactory<,>).MakeGenericType(t.GenericTypeArguments[0], t.GenericTypeArguments[2]);

				var readSingleModelRepository = c.Resolve(readSingleModelRepositoryType);
				var viewModelFactory = c.Resolve(viewModelFactoryType);

				var serviceType = typeof(SingleViewModelService<,,>).MakeGenericType(t.GenericTypeArguments[0], t.GenericTypeArguments[1], t.GenericTypeArguments[2]);
				var service = Activator.CreateInstance(serviceType, readSingleModelRepository, viewModelFactory);
				return service;
			}));

			container.RegisterType(typeof(ISingleViewModelService<>), new InjectionFactory((c, t, s) =>
			{
				var viewModelFactoryType = typeof(IViewModelFactory<>).MakeGenericType(t.GenericTypeArguments[0]);

				var viewModelFactory = c.Resolve(viewModelFactoryType);

				var serviceType = typeof(SingleViewModelService<>).MakeGenericType(t.GenericTypeArguments[0]);
				var service = Activator.CreateInstance(serviceType, viewModelFactory);
				return service;
			}));

			container.RegisterType(typeof(IListViewModelService<,>), new InjectionFactory((c, t, s) =>
			{
				var filterType = typeof(IQueryFilter<,>).MakeGenericType(t.GenericTypeArguments[1], t.GenericTypeArguments[0]);
				var readListModelRepositoryType = typeof(IReadListModelRepository<,>).MakeGenericType(t.GenericTypeArguments[1], t.GenericTypeArguments[0]);

				var filters = c.ResolveAll(filterType).AsEnumerable();
				var readListModelRepository = c.Resolve(readListModelRepositoryType);
				var pipeLine = c.Resolve<IPipeLineFactory>();
				var serviceType = typeof(ListViewModelService<,>).MakeGenericType(t.GenericTypeArguments[0], t.GenericTypeArguments[1]);
				var service = Activator.CreateInstance(serviceType, readListModelRepository, filters, pipeLine);
				return service;
			}));
		}
	}
}