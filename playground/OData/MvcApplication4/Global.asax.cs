using System.Collections.Generic;
using System.Web;

namespace MvcApplication4
{
    using System.Data.Services;
    using System.ServiceModel.Activation;
    using MvcTurbine.ComponentModel;
    using MvcTurbine.Ninject;
    using MvcTurbine.Web;

    public class MvcApplication : TurbineApplication
    {
        static MvcApplication()
        {
            ServiceLocatorManager.SetLocatorProvider(() => new NinjectServiceLocator());
        }
    }
}
