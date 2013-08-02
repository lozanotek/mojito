namespace Turon.DataServices {
	using System;
	using System.Data.Services;
	using System.Data.Services.Common;
	using System.Data.Services.Providers;
	using System.ServiceModel;
	using Turon.Core;

	[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
	public abstract class ServiceBase<TContext> : DataService<TContext>, IServiceProvider
		where TContext : IPackageContext {
		
		public TContext Context { get; set; }

		protected override TContext CreateDataSource() {
			return Context;
		}

		public static void InitializeService(DataServiceConfiguration config) {
			config.SetEntitySetAccessRule("Packages", EntitySetRights.AllRead);
			config.SetEntitySetPageSize("Packages", 40);
			config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
#if DEBUG
			config.UseVerboseErrors = true;
#endif
		}

		public object GetService(Type serviceType) {
			return serviceType == typeof(IDataServiceStreamProvider) ?
				new PackageStreamProvider(RouteProvider) : null;
		}

		protected abstract RouteProvider RouteProvider { get; }
	}
}
