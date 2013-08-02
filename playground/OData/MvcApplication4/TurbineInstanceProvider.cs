namespace MvcApplication4 {
	using System;
	using System.ServiceModel;
	using System.ServiceModel.Channels;
	using MvcTurbine.ComponentModel;

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