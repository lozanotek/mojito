namespace MvcApplication4.Services {
	using System;
	using System.Data.Services;
	using System.Data.Services.Common;
	using System.Data.Services.Providers;

	[System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)]
	public class Packages : DataService<PackageContext>, IServiceProvider {
		private PackageContext Context { get; set; }

		public Packages(PackageContext context) {
			Context = context;
		}

		// This method is called only once to initialize service-wide policies.
		public static void InitializeService(DataServiceConfiguration config) {
			config.SetEntitySetAccessRule("Packages", EntitySetRights.AllRead);
			config.SetEntitySetPageSize("Packages", 40);
			config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
#if DEBUG
			config.UseVerboseErrors = true;
#endif
		}

		protected override PackageContext CreateDataSource() {
			return Context;
		}

		public object GetService(Type serviceType) {
			return serviceType == typeof(IDataServiceStreamProvider) ? new PackageServiceStreamProvider() : null;
		}
	}
}
