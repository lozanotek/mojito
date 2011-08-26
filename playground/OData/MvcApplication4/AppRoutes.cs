namespace MvcApplication4 {
    using System.Web.Mvc;
    using System.Web.Routing;
    using MvcTurbine.Routing;

    public class AppRoutes : IRouteRegistrator {
        public void Register(RouteCollection routes) {
            //var serviceRoute = new ServiceRoute("nuget", HostFactoryBase, typeof(Packages)) {
            //    Defaults = new RouteValueDictionary { { "serviceType", "odata" } },
            //    Constraints = new RouteValueDictionary { { "serviceType", "odata" } }
            //};

            //RouteTable.Routes.Add(new DynamicServiceRoute("feed/{feedIdentifier}", 
            //    null, HostFactoryBase, typeof(Packages)));
            ////RouteTable.Routes.Add(new DynamicSyndicationRoute("RSS/{feedIdentifier}/{apiKey}", typeof(PackageRssFeedHandler)));

            //routes.Add("nuget", serviceRoute);

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}