namespace MvcApplication4 {
	using System.ServiceModel.Activation;
	using System.ServiceModel.Description;
	using MvcApplication4.Services;
	using MvcTurbine.ComponentModel;
	using Turon.Core;
	using Turon.DataServices.Packages;
	using Turon.Packages;
	using Turon.Recipes;
	using Turon.Routing;

	public class ServiceReg : IServiceRegistration {
		public void Register(IServiceLocator locator) {

			TurbineDataHostFactory.SetServiceLocator(locator);
			locator.Register<IFeedRouteService, FeedRouteService>();

			locator.Register<IServiceBehavior, TurbineServiceBehavior>();
			locator.Register<IServiceInstanceProvider, TurbineInstanceProvider>();
			locator.Register<ServiceHostFactoryBase, TurbineDataHostFactory>();
			locator.Register<PackageService, PackageService>();
			locator.Register<PackageContext, PackageContext>();
			locator.Register<RecipeContext, RecipeContext>();
			locator.Register<RecipeService, RecipeService>();
			locator.Register<IPackageRepository, StubPackageRepository>();
			locator.Register<IRecipeRepository, StubRecipeRepository>();
		}
	}
}