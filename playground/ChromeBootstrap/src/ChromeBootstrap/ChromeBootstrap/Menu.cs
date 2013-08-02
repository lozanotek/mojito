namespace Connect.Chrome.Bootstrap {
	using System.Collections.Generic;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Routing;

	public class Menu {
		private readonly List<MenuItem> _menuItems = new List<MenuItem>();

		public Menu(params MenuItem[] menuItems) {
			_menuItems.AddRange(menuItems);
		}

		public void AddItem(MenuItem menuItem) {
			_menuItems.Add(menuItem);
		}

		public IList<MenuItem> GetMenuItems() {
			return _menuItems;
		}
	}

	public static class MenuExtensions {
		public static UrlHelper Url(this Menu menu) {
			var requestContext = CreateRequestContext();
			return new UrlHelper(requestContext);
		}

		public static UrlHelper Url(this MenuItem item) {
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