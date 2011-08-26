namespace MvcApplication4 {
    using MvcApplication4.Services;
    using MvcTurbine.DataServices.Routing;

    public class  SvcRoutes : ServiceRouteRegistry {
        public SvcRoutes() {
            MapRoute<Packages>("nuget")
            .DynamicRoute<Packages>("feed/{feedIdentifier}", new {feedIdentifier = "all"});
        }
    }
}
