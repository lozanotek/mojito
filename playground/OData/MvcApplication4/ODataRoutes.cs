namespace MvcApplication4 {
	using System.ServiceModel.Activation;
	using System.Web.Mvc;
	using System.Web.Routing;
	using MvcTurbine.Routing;
	using Turon.DataServices.Packages;
	using Turon.DataServices.Recipes;
	using Turon.Routing;

	public class ODataRoutes : IRouteRegistrator {
		private ServiceHostFactoryBase HostFactoryBase { get; set; }

		public ODataRoutes(ServiceHostFactoryBase hostFactoryBase) {
			HostFactoryBase = hostFactoryBase;
		}

		public void Register(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			RouteTable.Routes.Add(new DynamicServiceRoute("feed/{feedIdentifier}",
			                                              null, HostFactoryBase, typeof(PackageService)));
			RouteTable.Routes.Add(new DynamicServiceRoute("recipe/{feedIdentifier}",
			                                              null, HostFactoryBase, typeof(RecipeService)));

			//RouteTable.Routes.Add(new DynamicSyndicationRoute("RSS/{feedIdentifier}/{apiKey}", typeof(PackageRssFeedHandler)));

			routes.MapRoute(
				RouteNames.Default,
				"{controller}/{action}/{id}",
				new { controller = "Home", action = "Index" }
				);

			routes.MapRoute(
				RouteNames.Recipes.Download,
				"recipes/{id}/{version}/download",
				new { controller = "Recipes", action = "Download" });

			routes.MapRoute(
				RouteNames.Packages.Download,
				"packages/{id}/{version}/download",
				new { controller = "Packages", action = "Download" });
		}
	}
}