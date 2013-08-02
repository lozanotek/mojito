namespace MvcApplication4.Services {
	using System;
	using System.Collections.Generic;
	using Turon.Core;
	using Turon.Domain;

	public class StubRecipeRepository : IRecipeRepository {
		public IEnumerable<Recipe> GetRecipes(string feedIdentifier) {
			IList<Recipe> list = new List<Recipe>();

			if (!string.IsNullOrEmpty(feedIdentifier)) {
				for (var i = 0; i < 10; i++) {
					var title = string.Format("Recipe {0} from {1}", (i + 1), feedIdentifier);
					var id = string.Format("{0}.{1}.recipe", (i + 1), feedIdentifier);

					var pkg = new Recipe {
						Id = id,
						Title = title,
						Version = "1.0.0",
						Authors = "Joe Smith",
						IsAbsoluteLatestVersion = true,
						IsLatestVersion = true,
						LastUpdated = DateTime.Now.AddHours(-4),
						Listed = true,
						PackageSize = 1024 * 4,
						Published = DateTime.Now.AddHours(-4),
						DownloadCount = 1,
						RequireLicenseAcceptance = false,
						Copyright = "2013",
						Description = "Demo",
						VersionDownloadCount = 1,
						Dependencies = "Ninject:3.0.1.10:|EntityFramework:5.0.0:"
					};

					list.Add(pkg);
				}
			}

			return list;
		}
	}
}