using AntonAir.CQRS.Infrastructure.Read.Interfaces.Services.List;
using AntonAir.DomainObjects.SearchCriteria;
using AntonAir.DomainObjects.ViewModel;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using AntonAir.App_Start;

using Microsoft.Practices.Unity;

namespace AntonAir.Controllers
{
	public class FlightController : ApiController
	{
		private readonly IListViewModelService<FlightSearchCriteria, FlightViewModel> searchService;

		public FlightController()
		{
			var container = UnityConfig.GetConfiguredContainer();
			this.searchService = container.Resolve<IListViewModelService<FlightSearchCriteria, FlightViewModel>>();
		}

		// GET api/flight
		[SwaggerOperation("Search")]
		[SwaggerResponse(HttpStatusCode.OK)]
		[SwaggerResponse(HttpStatusCode.NotFound)]
		public HttpResponseMessage Get([FromUri] Guid fromCityId, [FromUri] Guid toCityId, [FromUri] DateTime? departureDateTime, [FromUri] int ticketsAmount = 1)
		{
			var search = new FlightSearchCriteria
				             {
					             FromCityId = fromCityId,
					             ToCityId = toCityId,
					             Date = departureDateTime,
					             Amount = ticketsAmount
				             };

			var result = searchService.Get(search);

			return Request.CreateResponse(HttpStatusCode.Accepted, result);
		}
	}
}