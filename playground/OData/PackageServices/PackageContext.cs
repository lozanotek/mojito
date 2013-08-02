namespace Turon.DataServices.Packages {
	using System.Linq;
	using Turon.Core;
	using Turon.Domain;
	using Turon.Routing;

	public class PackageContext : IPackageContext {
		public IFeedRouteService RouteService { get; private set; }
		public IPackageRepository PackageRepository { get; set; }

		public PackageContext(IFeedRouteService routeService, IPackageRepository packageRepository) {
			RouteService = routeService;
			PackageRepository = packageRepository;
		}

		public IQueryable<Package> Packages {
			get {
				var feedIdentifier = RouteService.GetIdentifier();
				var packages = PackageRepository.GetPackages(feedIdentifier);
				return packages == null ? null : packages.AsQueryable();
			}
		}
	}
}
