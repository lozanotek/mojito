namespace Mojito.Server.Services {
    using System.Data.Services;
    using System.Data.Services.Common;

    [System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public abstract class MojitoDataService<TContext> : DataService<TContext> {
        public TContext Context { get; set; }

        protected MojitoDataService(TContext context) {
            Context = context;
        }

        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config) {
            config.SetEntitySetAccessRule("*", EntitySetRights.AllRead);
            config.SetEntitySetPageSize("*", 250);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;

#if DEBUG
            config.UseVerboseErrors = true;
#endif
        }

        protected override TContext CreateDataSource() {
            return Context;
        }
    }
}