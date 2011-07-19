namespace MvcApplication4.Routing
{
    using System;
    using System.ServiceModel.Activation;
    using System.Web;
    using System.Web.Routing;

    public class DynamicServiceRoute : RouteBase, IRouteHandler
    {
        private readonly string virtualPath;
        private readonly ServiceRoute innerServiceRoute;
        private readonly Route innerRoute;

        public DynamicServiceRoute(string pathPrefix, object defaults, ServiceHostFactoryBase serviceHostFactory, Type serviceType)
        {
            if (!pathPrefix.EndsWith("/"))
            {
                pathPrefix += "/";
            }
            pathPrefix += "{*servicePath}";

            virtualPath = string.Format("{0}-{1}/", serviceType.FullName, Guid.NewGuid());
            innerServiceRoute = new ServiceRoute(virtualPath, serviceHostFactory, serviceType);
            innerRoute = new Route(pathPrefix, new RouteValueDictionary(defaults), this);
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            return innerRoute.GetRouteData(httpContext);
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext) {

            var servicePath = requestContext.RouteData.Values["servicePath"] as string;
            var path = string.Format("~/{0}{1}", virtualPath, servicePath);
            requestContext.HttpContext.RewritePath(path, true);
            return innerServiceRoute.RouteHandler.GetHttpHandler(requestContext);
        }
    }
}