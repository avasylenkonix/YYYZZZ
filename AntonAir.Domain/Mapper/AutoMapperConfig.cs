using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AntonAir.Data.Entities;
using AntonAir.DomainObjects.ViewModel;

using AutoMapper;

namespace AntonAir.Domain.Mapper
{
	public class AutoMapperConfig
	{
		private static readonly object SyncRoot = new object();

		private static IMapper mapper;

		public static IMapper Mapper
		{
			get
			{
				if (mapper == null)
				{
					lock (SyncRoot)
					{
						if (mapper == null)
						{
							mapper = InitAutoMapperConfig();
						}
					}
				}

				return mapper;
			}
		}

		private static IMapper InitAutoMapperConfig()
		{
			MapperConfiguration configuration = new MapperConfiguration(
				c =>
					{
						c.CreateMap<City, CityViewModel>();
						c.CreateMap<Flight, FlightViewModel>();
					});

			return configuration.CreateMapper();
		}
	}
}
