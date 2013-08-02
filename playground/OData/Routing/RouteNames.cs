namespace Turon.Routing {
	public static class RouteNames {
		public const string Default = "Default";

		public static class Packages {
			public const string Search = "Packages.Search";
			public const string Upload = "Packages.Upload";
			public const string Delete = "Packages.Delete";
			public const string Info = "Packages.Info";
			public const string Download = "Packages.Download";
			public const string DownloadLatestVersion = "Packages.Download.Latest";
			public const string Feed = "OData Package Feed";
		}

		public static class Recipes {
			public const string Download = "Recipes.Download";
		}

		public const string TabCompletionPackageIds = "Package Manager Console Tab Completion - Package IDs";
		public const string TabCompletionPackageVersions = "Package Manager Console Tab Completion - Package Versions";
	}
}