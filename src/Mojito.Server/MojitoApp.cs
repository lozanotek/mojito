namespace Mojito.Server {
    using MvcTurbine.ComponentModel;
    using MvcTurbine.Ninject;
    using MvcTurbine.Web;
    using MvcTurbine.Web.Config;

    public class MojitoApp : TurbineApplication {
        static MojitoApp() {
            ServiceLocatorManager.SetLocatorProvider(() => new NinjectServiceLocator());

            Engine.Initialize
                .DisableHttpModuleRegistration();
        }
    }
}
