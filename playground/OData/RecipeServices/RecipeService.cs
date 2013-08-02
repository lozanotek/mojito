namespace Turon.DataServices.Recipes {
	using System.Web.Routing;
	using Turon.DataServices;
	using Turon.Routing;

	public class RecipeService : ServiceBase<RecipeContext> {
		public RecipeService(RecipeContext context) {
			Context = context;
		}

		protected override RouteProvider RouteProvider {
			get { return pkg => RouteTable.Routes[RouteNames.Recipes.Download]; }
		}
	}
}