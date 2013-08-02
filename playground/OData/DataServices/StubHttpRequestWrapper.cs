namespace Turon.DataServices {
	using System.IO;
	using System.Web;

	/// <summary>
	/// Prevents "System.Web.HttpException (0x80004005): This method or property is not
	/// supported after HttpRequest.GetBufferlessInputStream has been invoked." from being
	/// thrown at System.Web.HttpRequest.get_InputStream().
	/// </summary>
	class StubHttpRequestWrapper : HttpRequestWrapper {
		public StubHttpRequestWrapper(HttpRequest httpRequest)
			: base(httpRequest) {
		}

		public override Stream InputStream {
			get {
				return new MemoryStream();
			}
		}
	}
}