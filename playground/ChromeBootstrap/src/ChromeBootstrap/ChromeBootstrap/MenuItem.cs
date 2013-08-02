namespace Connect.Chrome.Bootstrap {
	using System;
	using System.Collections.Generic;

	public class MenuItem {
		private readonly List<MenuItem> _menuItems = new List<MenuItem>();

		public MenuItem(string title, string url, params MenuItem[] menuItems) : 
			this(title, () => url, menuItems) { }

		public MenuItem(string title, Func<string> urlProvider, params MenuItem[] menuItems) {
			Title = title;
			_menuItems.AddRange(menuItems);
			Url = urlProvider;
		}

		public string Title { get; set; }
		public Func<string> Url { get; set; }

		public void AddItem(MenuItem menuItem) {
			_menuItems.Add(menuItem);
		}

		public IList<MenuItem> GetMenuItems() {
			return _menuItems;
		}
	}
}
