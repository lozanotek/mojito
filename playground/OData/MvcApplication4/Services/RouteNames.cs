namespace MvcApplication4.Services {
	using System.Web.Routing;

	public static class RouteNames {
		public static readonly RouteValueDictionary PackageFeedRouteValues = new RouteValueDictionary { { "serviceType", "odata" } };

		public const string ApiExplorer = "ApiExplorer";
		public const string Indexing = "Indexing";

		public static class Users {
			public const string All = "Users.All";
			public const string ForUser = "Users.ForUser";
		}

		public static class Packages {
			public const string Search = "Packages.Search";
			public const string Upload = "Packages.Upload";
			public const string Delete = "Packages.Delete";
			public const string Info = "Packages.Info";
			public const string Download = "Packages.Download";
			public const string DownloadLatestVersion = "Packages.Download.Latest";
			public const string Feed = "OData Package Feed";
		}

		public const string TabCompletionPackageIds = "Package Manager Console Tab Completion - Package IDs";
		public const string TabCompletionPackageVersions = "Package Manager Console Tab Completion - Package Versions";
	}
}