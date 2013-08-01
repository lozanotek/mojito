namespace MvcApplication4.Services {
	using System;
	using System.Data.Services;
	using System.Data.Services.Common;
	using System.Data.Services.Providers;
	using System.Web.Routing;

	[System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)]
	public class Recipes : DataService<RecipeContext>, IServiceProvider {
		private RecipeContext Context { get; set; }

		public Recipes(RecipeContext context) {
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

		protected override RecipeContext CreateDataSource() {
			return Context;
		}

		public object GetService(Type serviceType) {
			return serviceType == typeof(IDataServiceStreamProvider) ?
				new PackageServiceStreamProvider(pkg => RouteTable.Routes[RouteNames.Recipes.Download]) : null;
		}
	}
}
