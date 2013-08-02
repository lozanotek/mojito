namespace MvcApplication4 {
	using System;
	using System.Data.Services;
	using System.ServiceModel.Description;
	using MvcTurbine.ComponentModel;

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
}