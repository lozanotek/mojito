namespace Turon.Routing {
	using System;
	using System.ServiceModel.Activation;
	using System.Web;
	using System.Web.Routing;

	public class DynamicServiceRoute : RouteBase, IRouteHandler {
		private string VirtualPath { get; set; }
		private ServiceRoute InnerServiceRoute { get; set; }
		private Route InnerRoute { get; set; }

		public DynamicServiceRoute(string pathPrefix, object defaults, ServiceHostFactoryBase serviceHostFactory, Type serviceType) {
			if (!pathPrefix.EndsWith("/")) {
				pathPrefix += "/";
			}
			pathPrefix += "{*servicePath}";

			var serviceName = serviceType.Name.Replace("Service", "");
			VirtualPath = string.Format("turon/{0}/", serviceName);

			InnerServiceRoute = new ServiceRoute(VirtualPath, serviceHostFactory, serviceType);
			InnerRoute = new Route(pathPrefix, new RouteValueDictionary(defaults), this);
		}

		public override RouteData GetRouteData(HttpContextBase httpContext) {
			return InnerRoute.GetRouteData(httpContext);
		}

		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values) {
			return null;
		}

		public IHttpHandler GetHttpHandler(RequestContext requestContext) {
			var servicePath = requestContext.RouteData.Values["servicePath"] as string;
			var path = string.Format("~/{0}{1}", VirtualPath, servicePath);

			requestContext.HttpContext.RewritePath(path, true);
			return InnerServiceRoute.RouteHandler.GetHttpHandler(requestContext);
		}
	}
}
