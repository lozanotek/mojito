namespace Mojito.Server.Services {
    using System.Web;
    using System.Web.Routing;

    public class FeedRouteService : IFeedRouteService {
        public string GetIdentifier() {
            var routeData = GetRouteData();
            if (routeData == null) return null;

            return routeData.Values["feedIdentifier"] as string;
        }

        protected RouteData GetRouteData() {
            var context = HttpContext.Current;
            if (context == null) return null;

            var wrapper = new HttpContextWrapper(HttpContext.Current);
            return wrapper.Request.RequestContext.RouteData;
        }
    }
}