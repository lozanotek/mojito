namespace MvcApplication4 {
	using System;
	using System.ServiceModel.Dispatcher;

	public interface IServiceInstanceProvider : IInstanceProvider {
		void SetServiceType(Type serviceType);
		Type ServiceType { get; }
	}
}