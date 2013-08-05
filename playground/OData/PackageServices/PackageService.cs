namespace Turon.DataServices.Packages {
	using System.ServiceModel;
	using System.Web.Routing;
	using Turon.DataServices;
	using Turon.Packages;
	using Turon.Routing;

	[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
	public class PackageService : ServiceBase<PackageContext> {
		public PackageService(PackageContext context) {
			Context = context;
		}

		protected override RouteProvider RouteProvider {
			get { return pkg => RouteTable.Routes[RouteNames.Packages.Download]; }
		}
	}
}
