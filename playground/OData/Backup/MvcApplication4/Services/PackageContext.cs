namespace MvcApplication4.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Routing;
    using MvcApplication4.Routing;

    public interface IFeedRouteService {
        string GetIdentifier();
    }

    public class FeedRouteService : IFeedRouteService {
        public string GetIdentifier() {
            if (HttpContext.Current != null) {
                var wrapper = new HttpContextWrapper(HttpContext.Current);
                if (!wrapper.Request.RequestContext.RouteData.Values.ContainsKey("feedIdentifier"))
                {
                    return null;
                }

                return wrapper.Request.RequestContext.RouteData.Values["feedIdentifier"] as string;
            }

            return null;
        }
    }

    public class PackageContext
    {
        public IFeedRouteService RouteService { get; private set; }

        public PackageContext(IFeedRouteService routeService) {
            RouteService = routeService;
        }

        public IQueryable<Package> Packages {
            get {
                // Determine identifier
                var feedIdentifier = RouteService.GetIdentifier();

                IList<Package> list = new List<Package>();

                if (!string.IsNullOrEmpty(feedIdentifier)) {
                    for (var i = 0; i < 10; i++) {
                        var title = string.Format("Package {0} from {1}", (i + 1), feedIdentifier);
                        var pkg = new Package {Id = title, Title = title, Version = "1.0.0.0"};
                        list.Add(pkg);
                    }
                }

                return list.AsQueryable();
            }
        }
    }
}
