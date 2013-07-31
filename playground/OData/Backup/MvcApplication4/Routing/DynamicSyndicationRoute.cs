namespace MvcApplication4.Routing
{
    using System;
    using System.Web;
    using System.Web.Routing;

    public class DynamicSyndicationRoute : RouteBase, IRouteHandler
    {
        private readonly Type handlerType;
        private readonly string virtualPath;
        private readonly Route innerRoute;

        public DynamicSyndicationRoute(string pathPrefix, Type handlerType)
        {
            this.handlerType = handlerType;

            if (!pathPrefix.EndsWith("/"))
            {
                pathPrefix += "/";
            }
            pathPrefix += "{*servicePath}";

            virtualPath = string.Format("{0}-{1}/", handlerType.FullName, Guid.NewGuid());
            innerRoute = new Route(pathPrefix, new RouteValueDictionary(), this);
        }

        public static RouteData GetCurrentRouteData()
        {
            if (HttpContext.Current != null)
            {
                var wrapper = new HttpContextWrapper(HttpContext.Current);
                return wrapper.Request.RequestContext.RouteData;
            }
            return null;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            return innerRoute.GetRouteData(httpContext);
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            requestContext.HttpContext.RewritePath(string.Format("~/{0}{1}", virtualPath, requestContext.RouteData.Values["servicePath"]), true);

            IHttpHandler handler = Activator.CreateInstance(handlerType) as IHttpHandler;
            requestContext.HttpContext.Items["RouteData"] = requestContext.RouteData;
            return handler;
        }
    }
}