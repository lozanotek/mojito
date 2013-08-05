namespace Turon.Packages {
	using System.Collections.Generic;
	using System.IO;
	using System.Web.Mvc;
	using NuGet;

	public class PackagesController : Controller {
		public ActionResult DownloadPackage(string id, string version) {
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
