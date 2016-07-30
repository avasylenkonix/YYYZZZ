using System.Web.Http;

using AntonAir.IoC;

namespace AntonAir
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services
			Bootstrapper.Bootstrap();

			// Web API routes
			config.MapHttpAttributeRoutes();
			
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}