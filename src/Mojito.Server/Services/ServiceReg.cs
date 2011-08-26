namespace Mojito.Server.Services
{
    using Mojito.Server.Models;
    using MvcTurbine.ComponentModel;

    public class ServiceReg : IServiceRegistration {
        public void Register(IServiceLocator locator) {

            locator.Register<IFeedRouteService, FeedRouteService>();

            locator.Register<PackageService, PackageService>();
            locator.Register<NuGetContext, NuGetContext>();
            locator.Register<MojitoContext, MojitoContext>();

        }
    }
}