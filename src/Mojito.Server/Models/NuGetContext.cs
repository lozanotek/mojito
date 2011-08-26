namespace Mojito.Server.Models {
    using System.Linq;
    using System.Collections.Generic;
    using Mojito.Server.Services;

    public class NuGetContext {
        public IFeedRouteService RouteService { get; private set; }

        public NuGetContext(IFeedRouteService routeService) {
            RouteService = routeService;
        }

        public IQueryable<PackageServiceModel> Packages {
            get {
                // Determine identifier
                var feedIdentifier = RouteService.GetIdentifier();

                IList<PackageServiceModel> list = new List<PackageServiceModel>();

                if (!string.IsNullOrEmpty(feedIdentifier)) {
                    for (var i = 0; i < 10; i++) {
                        var title = string.Format("Package {0} from {1}", (i + 1), feedIdentifier);
                        var pkg = new PackageServiceModel {Id = title, Title = title, Version = "1.0.0.0"};
                        list.Add(pkg);
                    }
                }

                return list.AsQueryable();
            }
        }
    }
}
