namespace MvcApplication4.Services {
	using System.Web;

	/// <summary>
	/// Allow HttpContext.Request to be replaced with an arbitrary HttpRequestBase instance.
	/// </summary>
	class HttpContextWrapperWithRequest : HttpContextWrapper {
		private readonly HttpRequestBase request;

		public HttpContextWrapperWithRequest(HttpContext httpContext, HttpRequestBase request)
			: base(httpContext) {
			this.request = request;
		}

		public override HttpRequestBase Request {
			get {
				return request;
			}
		}
	}
}