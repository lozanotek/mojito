namespace Turon.DataServices.Recipes {
	using System.Linq;
	using Turon.Core;
	using Turon.Domain;
	using Turon.Routing;

	public class RecipeContext : IPackageContext {
		public IFeedRouteService RouteService { get; private set; }
		public IRecipeRepository RecipeRepository { get; private set; }

		public RecipeContext(IFeedRouteService routeService, IRecipeRepository recipeRepository) {
			RouteService = routeService;
			RecipeRepository = recipeRepository;
		}

		public IQueryable<Package> Packages {
			get {
				var feedIdentifier = RouteService.GetIdentifier();
				var recipes = RecipeRepository.GetRecipes(feedIdentifier);
				return recipes == null ? null : 
					recipes.Select(recipe => recipe.ToPackage()).AsQueryable();
			}
		}
	}
}
