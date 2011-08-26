namespace MvcApplication4
{
    using System.ServiceModel.Activation;
    using System.ServiceModel.Description;
    using MvcTurbine.ComponentModel;
    using Services;

    public class ServiceReg : IServiceRegistration {
        public void Register(IServiceLocator locator) {

            //TurbineDataHostFactory.SetServiceLocator(locator);
            locator.Register<IFeedRouteService, FeedRouteService>();

            //locator.Register<IServiceBehavior, TurbineServiceBehavior>();
            //locator.Register<IServiceInstanceProvider, TurbineInstanceProvider>();
            //locator.Register<ServiceHostFactoryBase, TurbineDataHostFactory>();
            locator.Register<Packages, Packages>();
            locator.Register<PackageContext, PackageContext>();
        }
    }
}