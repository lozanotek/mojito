namespace MvcApplication4.Services
{
    using System;
    using System.Web.Routing;

    public class PackageHandler : IPackageHandler
    {
        private readonly IMyGetPackageRepository packageRepository = new MyGetPackageRepository(new BlobStoragePackageStorageProvider(), new PackageDependencyParser());
        private readonly FeedSecurity feedSecurity = new FeedSecurity(new FeedPrivilegesRepository(new UserRepository(new ClaimsHelper()), new MyGetFeedRepository()));

        public void CreatePackage(RequestContext context)
        {
            // Get route data
            RouteData routeData = context.RouteData;

            // Get data from the route
            string feedIdentifier = routeData.GetRequiredString("feedIdentifier");
            string apiKey = routeData.GetRequiredString("apiKey");

            // Authenticate
            if (!feedSecurity.Authenticate(context.HttpContext, feedIdentifier, apiKey))
            {
                return;
            }

            NuGetPackage package;
            if (packageRepository.TryAddPackage(context.HttpContext.Request.InputStream, feedIdentifier, out package))
            {
                // Evict the cache   
                var cacheService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(ICacheService)) as ICacheService;
                cacheService.Remove(string.Format("packages_{0}", feedIdentifier));
            }
        }

        public void PublishPackage(RequestContext context)
        {
            // MyGet considers this the same as CreatePackage
            CreatePackage(context);
        }

        public void DeletePackage(RequestContext context)
        {
            // Only accept delete requests
            if (!context.HttpContext.Request.HttpMethod.Equals("DELETE", StringComparison.OrdinalIgnoreCase))
            {
                context.HttpContext.Response.StatusCode = 404;
                return;
            }

            // Extract route values
            RouteData routeData = context.RouteData;

            // Extract the apiKey, packageId and make sure the version if a valid version string
            // (fail to parse if it's not)
            string feedIdentifier = routeData.GetRequiredString("feedIdentifier");
            string apiKey = routeData.GetRequiredString("apiKey");
            string packageId = routeData.GetRequiredString("packageId");
            Version version = new Version(routeData.GetRequiredString("version"));

            // Authenticate
            if (!feedSecurity.Authenticate(context.HttpContext, feedIdentifier, apiKey))
            {
                return;
            }

            packageRepository.DeletePackage(feedIdentifier, packageId, version.ToString());

            // Evict the cache   
            var cacheService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(ICacheService)) as ICacheService;
            cacheService.Remove(string.Format("packages_{0}", feedIdentifier));
        }
    }
}