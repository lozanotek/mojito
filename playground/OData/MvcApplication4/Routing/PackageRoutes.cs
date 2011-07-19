using System.Web.Routing;
using MyGet.Server.Services;
using RouteMagic;

namespace MyGet.Server.Routing
{
    using MvcApplication4.Services;

    public static class PackageRoutes
    {
        public static void Register()
        {
            // Route to create a new package
            RouteTable.Routes.MapDelegate("CreatePackage",
                "F/{feedIdentifier}/PackageFiles/{apiKey}/nupkg",
                context => CreatePackageHandler().CreatePackage(context));

            // Route to publish a package
            RouteTable.Routes.MapDelegate("PublishPackage",
                "F/{feedIdentifier}/PublishedPackages/Publish",
                context => CreatePackageHandler().PublishPackage(context));

            // Route to delete packages
            RouteTable.Routes.MapDelegate("DeletePackage",
                "F/{feedIdentifier}/Packages/{apiKey}/{packageId}/{version}",
                context => CreatePackageHandler().DeletePackage(context));
        }

        private static IPackageHandler CreatePackageHandler()
        {
            return new PackageHandler();
        }
    }
}
