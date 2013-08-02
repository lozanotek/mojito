namespace Connect.Chrome.Bootstrap {
	using System;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Routing;

	public static class MenuManager {
		private static Func<Menu> menuProvider;
		private static Func<UrlHelper> urlProvider;

		static MenuManager() {
			SetUrlProvider(DefaultUrlProvider);
		}

		public static void SetMenuProvider(Func<Menu> provider) {
			menuProvider = provider;
		}

		public static void SetUrlProvider(Func<UrlHelper> provider) {
			urlProvider = provider;
		}

		public static Menu AppMenu {
			get {
				if (menuProvider == null) {
					throw new InvalidOperationException("Menu Provider is not initialized.");
				}

				return menuProvider();
			}
		}

		public static UrlHelper UrlProvider {
			get {
				if(urlProvider == null) {
					throw new InvalidOperationException("Url Provider is not initialized.");
				}

				return urlProvider();
			}
		}

		private static UrlHelper DefaultUrlProvider() {
			var requestContext = CreateRequestContext();
			return new UrlHelper(requestContext);
		}

		private static RequestContext CreateRequestContext() {
			var context = HttpContext.Current;
			var contextBase = new HttpContextWrapper(context);
			var data = RouteTable.Routes.GetRouteData(contextBase);
			return new RequestContext(contextBase, data);
		}
	}
}
