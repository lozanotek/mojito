namespace MvcApplication4 {
	using System;
	using System.Data.Services;
	using System.ServiceModel;
	using MvcTurbine.ComponentModel;

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
}