namespace MvcApplication4.Services {
	using System.Web;

	public class FeedRouteService : IFeedRouteService {
		public string GetIdentifier() {
			if (HttpContext.Current != null) {
				var wrapper = new HttpContextWrapper(HttpContext.Current);
				var routeValues = wrapper.Request.RequestContext.RouteData.Values;

				if (!routeValues.ContainsKey("feedIdentifier")) {
					return null;
				}

				return routeValues["feedIdentifier"] as string;
			}

			return null;
		}
	}
}