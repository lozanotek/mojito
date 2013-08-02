namespace Turon.Core {
	using System.Collections.Generic;
	using Turon.Domain;

	public interface IRecipeRepository {
		IEnumerable<Recipe> GetRecipes(string feedIdentifier);
	}
}
