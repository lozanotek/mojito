namespace Turon.Services.Controllers {
	using System.Collections.Generic;
	using System.IO;
	using System.Web.Mvc;
	using NuGet;

	public class RecipesController : Controller {
		public ActionResult Download(string version, string id) {
			var metadata = new ManifestMetadata {
				Authors = "John Smith",
				Version = version,
				Id = id,
				Description = "A description",
				DependencySets = new List<ManifestDependencySet>()
			};

			var set = new ManifestDependencySet {
				Dependencies = new List<ManifestDependency> {
					new ManifestDependency {
						Id = "Ninject", Version = "3.0.1.10"
					},
					new ManifestDependency {
						Id = "EntityFramework", Version = "5.0.0"
					}
				}
			};

			metadata.DependencySets.Add(set);

			var builder = new PackageBuilder();
			builder.Populate(metadata);

			var packageStream = new MemoryStream();
			builder.Save(packageStream);
			packageStream.Seek(0, SeekOrigin.Begin);
			return File(packageStream, "application/zip");
		}
	}
}