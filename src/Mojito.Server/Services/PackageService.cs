namespace Mojito.Server.Services {
    using System;
    using System.Data.Services;
    using System.Data.Services.Providers;
    using System.IO;
    using Mojito.Server.Models;
    
    public class PackageService : MojitoDataService<NuGetContext>, IDataServiceStreamProvider, IServiceProvider {
        public PackageService(NuGetContext context) : base(context) {
            Context = context;
        }

        public void DeleteStream(object entity, DataServiceOperationContext operationContext) {
            throw new NotSupportedException();
        }

        public Stream GetReadStream(object entity, string etag, bool? checkETagForEquality, DataServiceOperationContext operationContext) {
            throw new NotSupportedException();
        }

        public Uri GetReadStreamUri(object entity, DataServiceOperationContext operationContext) {
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
            return serviceType == typeof(IDataServiceStreamProvider) ? this : null;
        }
    }
}