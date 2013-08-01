namespace MvcApplication4.Services {
	using System;
	using System.Linq;
	using System.Collections.Generic;

	public class PackageContext {
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
						var id = string.Format("Package.{0}.{1}", (i + 1), feedIdentifier);

						var pkg = new Package {
							Id = id,
							Title = title,
							Version = "1.0.0.0",
							Authors = "Joe Smith",
							IsAbsoluteLatestVersion =  true,
							IsLatestVersion = true,
							LastUpdated = DateTime.Now.AddHours(-4),
							Listed = true,
							PackageSize = 1024*3,
							Published = DateTime.Now.AddHours(-4),
							DownloadCount = 1,
							RequireLicenseAcceptance = false,
							Copyright = "2013",
							Description = "Demo",
							VersionDownloadCount = 1
						};

						list.Add(pkg);
					}
				}

				return list.AsQueryable();
			}
		}
	}
}
