namespace MvcApplication4.Services {
	using System;
	using System.Data.Services;
	using System.Web;
	using System.Web.Routing;

	public class PackageServiceStreamProvider : DefaultServiceStreamProvider {
		public PackageServiceStreamProvider() {
			ContentType = "application/zip";
		}

		public override Uri GetReadStreamUri(object entity, DataServiceOperationContext operationContext) {
			var package = (Package)entity;
			var vpath = GetPackageDownloadPath(package);
			return new Uri(operationContext.AbsoluteRequestUri, vpath);
		}

		public string GetPackageDownloadPath(Package package) {
			var route = RouteTable.Routes[RouteNames.Packages.Download];
			var routeValues = new { id = package.Id, version = package.Version, httproute = true };
			var pathData = route.GetVirtualPath(RequestContext, new RouteValueDictionary(routeValues)) ?? new VirtualPathData(route, "~");
			var path = pathData.VirtualPath;
			return VirtualPathUtility.ToAbsolute("~/" + path);
		}

		private static RequestContext RequestContext {
			get {
				var httpContext = HttpContext.Current;
				var request = new EmptyInputStreamHttpRequestWrapper(httpContext.Request);
				return new RequestContext(new HttpContextWrapperWithRequest(httpContext, request), new RouteData());
			}
		}
	}
}