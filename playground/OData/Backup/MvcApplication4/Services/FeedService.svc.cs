namespace MvcApplication4.Services {
    using System;
    using System.Data.Services;
    using System.Data.Services.Common;
    using System.Data.Services.Providers;
    using System.IO;

    // Disabled for live service
    [System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Packages : DataService<PackageContext>, IDataServiceStreamProvider, IServiceProvider {
        private PackageContext Context { get; set; }

        public Packages(PackageContext context)
        {
            Context = context;
        }

        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config) {
            config.SetEntitySetAccessRule("Packages", EntitySetRights.AllRead);
            config.SetEntitySetPageSize("Packages", 250);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;

#if DEBUG
            config.UseVerboseErrors = true;
#endif
        }

        protected override PackageContext CreateDataSource()
        {
            return Context;
        }

        public void DeleteStream(object entity, DataServiceOperationContext operationContext) {
            throw new NotSupportedException();
        }

        public Stream GetReadStream(object entity, string etag, bool? checkETagForEquality, DataServiceOperationContext operationContext) {
            throw new NotSupportedException();
        }

        public Uri GetReadStreamUri(object entity, DataServiceOperationContext operationContext) {
            var package = (Package)entity;

            return null;
        }

        public string GetStreamContentType(object entity, DataServiceOperationContext operationContext) {
            return "application/zip";
        }

        public string GetStreamETag(object entity, DataServiceOperationContext operationContext) {
            return null;
        }

        public Stream GetWriteStream(object entity, string etag, bool? checkETagForEquality, DataServiceOperationContext operationContext) {
            throw new NotSupportedException();
        }

        public string ResolveType(string entitySetName, DataServiceOperationContext operationContext) {
            throw new NotSupportedException();
        }

        public int StreamBufferSize {
            get {
                return 64000;
            }
        }

        public object GetService(Type serviceType) {
            if (serviceType == typeof(IDataServiceStreamProvider)) {
                return this;
            }
            return null;
        }
    }
}
