namespace Turon.DataServices {
	using System;
	using System.Data.Services;
	using System.Web;
	using System.Web.Routing;
	using Turon.Domain;

	public class PackageStreamProvider : BaseServiceStreamProvider {
		public RouteProvider RouteProvider { get; private set; }

		public PackageStreamProvider(RouteProvider routeProvider) {
			RouteProvider = routeProvider;
			ContentType = "application/zip";
		}

		public override Uri GetReadStreamUri(object entity, DataServiceOperationContext operationContext) {
			var package = (Package)entity;
			var vpath = GetPackageDownloadPath(package);
			return new Uri(operationContext.AbsoluteRequestUri, vpath);
		}

		private string GetPackageDownloadPath(Package package) {
			var route = RouteProvider(package);
			var routeValues = new { id = package.Id, version = package.Version };
			var pathData = route.GetVirtualPath(RequestContext, new RouteValueDictionary(routeValues)) ?? new VirtualPathData(route, "~");
			var path = pathData.VirtualPath;

			return VirtualPathUtility.ToAbsolute("~/" + path);
		}

		private static RequestContext RequestContext {
			get {
				var httpContext = HttpContext.Current;
				var request = new StubHttpRequestWrapper(httpContext.Request);
				return new RequestContext(new StubHttpContextWrapper(httpContext, request), new RouteData());
			}
		}
	}
}
