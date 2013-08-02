namespace Connect.Chrome.App_Start {
	using Connect.Chrome.Bootstrap;

	public static class MenuConfig {
		public static void InitializeMenu() {
			// Build the menu using the application's initial context
			MenuManager.SetMenuProvider(BuildStaticMenu);
		}

		private static Menu BuildStaticMenu() {
			// Get the provider for the current context
			var urlProvider = MenuManager.UrlProvider;

			// Build the simple menu
			var menu =
				new Menu(
					new MenuItem("Home","#",
						new MenuItem("Main", urlProvider.Action("Index", "Home")),
						new MenuItem("About", urlProvider.Action("About", "Home"))
					)
				);

			return menu;
		}
	}
}
