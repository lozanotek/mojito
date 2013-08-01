using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApplication4 {
	using System.Collections.ObjectModel;
	using System.Data.Services;
	using System.ServiceModel;
	using System.ServiceModel.Activation;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Description;
	using System.ServiceModel.Dispatcher;
	using MvcApplication4.Routing;
	using MvcTurbine.ComponentModel;
	using MvcTurbine.Ninject;
	using MvcTurbine.Routing;
	using MvcTurbine.Web;
	using Services;

	public class MvcApplication : TurbineApplication {
		static MvcApplication() {
			ServiceLocatorManager.SetLocatorProvider(() => new NinjectServiceLocator());
		}
	}

	public class ServiceReg : IServiceRegistration {
		public void Register(IServiceLocator locator) {

			TurbineDataHostFactory.SetServiceLocator(locator);
			locator.Register<IFeedRouteService, FeedRouteService>();

			locator.Register<IServiceBehavior, TurbineServiceBehavior>();
			locator.Register<IServiceInstanceProvider, TurbineInstanceProvider>();
			locator.Register<ServiceHostFactoryBase, TurbineDataHostFactory>();
			locator.Register<Packages, Packages>();
			locator.Register<PackageContext, PackageContext>();
			locator.Register<RecipeContext, RecipeContext>();
			locator.Register<Recipes, Recipes>();
		}
	}

	public class ODataRoutes : IRouteRegistrator {
		private ServiceHostFactoryBase HostFactoryBase { get; set; }

		public ODataRoutes(ServiceHostFactoryBase hostFactoryBase) {
			HostFactoryBase = hostFactoryBase;
		}

		public void Register(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			RouteTable.Routes.Add(new DynamicServiceRoute("feed/{feedIdentifier}", 
				null, HostFactoryBase, typeof(Packages)));
			RouteTable.Routes.Add(new DynamicServiceRoute("recipe/{feedIdentifier}", 
				null, HostFactoryBase, typeof(Recipes)));

			//RouteTable.Routes.Add(new DynamicSyndicationRoute("RSS/{feedIdentifier}/{apiKey}", typeof(PackageRssFeedHandler)));

			routes.MapRoute(
				RouteNames.Default,
				"{controller}/{action}/{id}",
				new { controller = "Home", action = "Index" }
			);

			routes.MapRoute(
				RouteNames.Recipes.Download,
				"recipes/{id}/{version}/content",
				new { controller = "Recipes", action = "Download" });

			routes.MapRoute(
				RouteNames.Packages.Download,
				"packages/{id}/{version}/content",
				new { controller = "Packages", action = "Download" });
		}
	}

	public class TurbineDataHostFactory : DataServiceHostFactory {
		public static IServiceLocator ServiceLocator { get; private set; }

		public static void SetServiceLocator(IServiceLocator locator) {
			ServiceLocator = locator;
		}

		public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses) {
			var serviceType = Type.GetType(constructorString, false);
			return CreateServiceHost(serviceType, baseAddresses);
		}

		protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses) {
			TurbineServiceHost.SetServiceLocator(ServiceLocator);
			return new TurbineServiceHost(serviceType, baseAddresses);
		}
	}

	public class TurbineServiceHost : DataServiceHost {
		public TurbineServiceHost(Type serviceType, params Uri[] baseAddresses) :
			base(serviceType, baseAddresses) {
		}

		protected override void OnOpening() {
			var serviceBehavior = ServiceLocator.Resolve<IServiceBehavior>();
			Description.Behaviors.Add(serviceBehavior);

			base.OnOpening();
		}

		public static void SetServiceLocator(IServiceLocator locator) {
			ServiceLocator = locator;
		}

		public static IServiceLocator ServiceLocator { get; private set; }
	}

	public class TurbineServiceBehavior : IServiceBehavior {
		public IServiceInstanceProvider InstanceProvider { get; private set; }

		public TurbineServiceBehavior(IServiceInstanceProvider instanceProvider) {
			InstanceProvider = instanceProvider;
		}

		///<summary>
		///Provides the ability to inspect the service host and the service description to confirm that the service can run successfully.
		///</summary>
		///<param name="serviceHostBase">The service host that is currently being constructed.</param>
		///<param name="serviceDescription">The service description.</param>
		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { }


		///<summary>
		///Provides the ability to pass custom data to binding elements to support the contract implementation.
		///</summary>
		///<param name="serviceHostBase">The host of the service.</param>
		///<param name="bindingParameters">Custom objects to which binding elements have access.</param>
		///<param name="serviceDescription">The service description of the service.</param>
		///<param name="endpoints">The service endpoints.</param>
		public void AddBindingParameters(
			ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
			Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters) { }

		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) {
			var endpointDispatchers =
				serviceHostBase.ChannelDispatchers.OfType<ChannelDispatcher>()
					.SelectMany(channelDispatcher => channelDispatcher.Endpoints);

			InstanceProvider.SetServiceType(serviceDescription.ServiceType);

			foreach (var endpointDispatcher in endpointDispatchers) {
				endpointDispatcher.DispatchRuntime.InstanceProvider = InstanceProvider;
			}
		}
	}

	public interface IServiceInstanceProvider : IInstanceProvider {
		void SetServiceType(Type serviceType);
		Type ServiceType { get; }
	}

	public class TurbineInstanceProvider : IServiceInstanceProvider {
		public IServiceLocator ServiceLocator { get; private set; }

		public TurbineInstanceProvider(IServiceLocator serviceLocator) {
			ServiceLocator = serviceLocator;
		}

		public void SetServiceType(Type serviceType) {
			ServiceType = serviceType;
		}

		public Type ServiceType { get; private set; }

		/// <summary>
		/// Returns a service object given the specified <see cref="T:System.ServiceModel.InstanceContext"></see> object.
		/// </summary>
		/// 
		/// <returns>
		/// A user-defined service object.
		/// </returns>
		/// 
		/// <param name="instanceContext">The current <see cref="T:System.ServiceModel.InstanceContext"></see> object.</param>
		public object GetInstance(InstanceContext instanceContext) {
			return GetInstance(instanceContext, null);
		}

		/// <summary>
		/// Returns a service object given the specified <see cref="T:System.ServiceModel.InstanceContext"></see> object.
		/// </summary>
		/// 
		/// <returns>
		/// The service object.
		/// </returns>
		/// 
		/// <param name="message">The message that triggered the creation of a service object.</param>
		/// <param name="instanceContext">The current <see cref="T:System.ServiceModel.InstanceContext"></see> object.</param>
		public object GetInstance(InstanceContext instanceContext, Message message) {
			return ServiceLocator.Resolve(ServiceType);
		}

		/// <summary>
		/// Called when an <see cref="T:System.ServiceModel.InstanceContext"></see> object recycles a service object.
		/// </summary>
		/// 
		/// <param name="instanceContext">The service's instance context.</param>
		/// <param name="instance">The service object to be recycled.</param>
		public void ReleaseInstance(InstanceContext instanceContext, object instance) {
			var disposable = instance as IDisposable;
			if (disposable == null)
				return;

			disposable.Dispose();
		}
	}
}
