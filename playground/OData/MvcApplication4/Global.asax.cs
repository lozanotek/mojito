
namespace MvcApplication4 {
	using MvcTurbine.ComponentModel;
	using MvcTurbine.Ninject;
	using MvcTurbine.Web;

	public class MvcApplication : TurbineApplication {
		static MvcApplication() {
			ServiceLocatorManager.SetLocatorProvider(() => new NinjectServiceLocator());
		}
	}
}
