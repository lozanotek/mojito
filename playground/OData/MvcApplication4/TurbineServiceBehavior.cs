namespace MvcApplication4 {
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.ServiceModel;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Description;
	using System.ServiceModel.Dispatcher;

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
}