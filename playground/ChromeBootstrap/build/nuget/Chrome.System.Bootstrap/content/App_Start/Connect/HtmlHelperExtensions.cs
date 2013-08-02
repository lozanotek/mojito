namespace Connect.Chrome.App_Start {
	using System.Collections.Generic;
	using System.Web.Mvc;
	using Connect.Chrome.Bootstrap;

	public static class HtmlHelperExtensions {
		public static MvcHtmlString GenereateMenu(this HtmlHelper helper, string cssClass = "nav") {
			var menu = MenuManager.AppMenu;
			var menuTag = BuildMenuItems(menu.GetMenuItems());
			menuTag.AddCssClass(cssClass);

			return new MvcHtmlString(menuTag.ToString());
		}

		private static TagBuilder BuildMenuItems(IList<MenuItem> items, bool addDivider = true) {
			if(items == null || items.Count == 0) return null;

			var menuList= new TagBuilder("ul");

			foreach (var menuItem in items) {
				var liTag = new TagBuilder("li");
				var refTag = new TagBuilder("a");
				refTag.Attributes.Add("href", menuItem.Url());
				refTag.SetInnerText(menuItem.Title);

				var subMenus = menuItem.GetMenuItems();
				if(subMenus != null && subMenus.Count != 0) {
					var subMenuTag = BuildMenuItems(subMenus, false);

					if(subMenuTag != null) {
						refTag.Attributes["href"] = "#";
						refTag.AddCssClass("dropdown-toggle");
						refTag.Attributes["data-toggle"] = "dropdown";

						var caretTag = new TagBuilder("b");
						caretTag.AddCssClass("caret");
						refTag.InnerHtml += caretTag;

						subMenuTag.AddCssClass("dropdown-menu");

						var menuClass = addDivider ? "dropdown" : "dropdown-submenu";
						liTag.AddCssClass(menuClass);
						liTag.InnerHtml += subMenuTag;
					}
				}

				// Add the link item
				liTag.InnerHtml += refTag;
				menuList.InnerHtml += liTag;

				if (!addDivider) {
					continue;
				}

				// Add the separator
				var dividerTag = new TagBuilder("li");
				dividerTag.AddCssClass("divider-vertical");

				menuList.InnerHtml += dividerTag;
			}

			return menuList;
		}
	}
}
