namespace Turon.DataServices {
	using System.Web;

	/// <summary>
	/// Allow HttpContext.Request to be replaced with an arbitrary HttpRequestBase instance.
	/// </summary>
	class StubHttpContextWrapper : HttpContextWrapper {
		private readonly HttpRequestBase request;

		public StubHttpContextWrapper(HttpContext httpContext, HttpRequestBase request)
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