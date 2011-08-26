namespace Mojito.Server.Services {
    using MvcTurbine.DataServices.Routing;

    public class  ServiceRoutes : ServiceRouteRegistry {
        public ServiceRoutes() {
            MapRoute<PackageService>("nuget")
                .DynamicRoute<PackageService>("feed/{feedIdentifier}", new {feedIdentifier = "all"})
                .DynamicRoute<InfoService>("info", null);
        }
    }
}
