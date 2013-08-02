namespace Turon.Routing {
	using System;
	using System.Web;
	using System.Web.Routing;

	public class DynamicSyndicationRoute : RouteBase, IRouteHandler {
		private Type HandlerType { get; set; }
		private string VirtualPath { get; set; }
		private Route InnerRoute { get; set; }

		public DynamicSyndicationRoute(string pathPrefix, Type handlerType) {
			HandlerType = handlerType;

			if (!pathPrefix.EndsWith("/")) {
				pathPrefix += "/";
			}
			pathPrefix += "{*servicePath}";

			var serviceName = handlerType.Name.Replace("Service", "");
			VirtualPath = string.Format("turon/{0}/", serviceName);
			InnerRoute = new Route(pathPrefix, new RouteValueDictionary(), this);
		}

		public static RouteData GetCurrentRouteData() {
			if (HttpContext.Current != null) {
				var wrapper = new HttpContextWrapper(HttpContext.Current);
				return wrapper.Request.RequestContext.RouteData;
			}
			return null;
		}

		public override RouteData GetRouteData(HttpContextBase httpContext) {
			return InnerRoute.GetRouteData(httpContext);
		}

		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values) {
			return null;
		}

		public IHttpHandler GetHttpHandler(RequestContext requestContext) {
			requestContext.HttpContext.RewritePath(string.Format("~/{0}{1}", VirtualPath, requestContext.RouteData.Values["servicePath"]), true);

			var handler = Activator.CreateInstance(HandlerType) as IHttpHandler;
			requestContext.HttpContext.Items["RouteData"] = requestContext.RouteData;
			return handler;
		}
	}
}