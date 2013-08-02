namespace Turon.Core {
	using System.Collections.Generic;
	using Turon.Domain;

	public interface IPackageRepository {
		IEnumerable<Package> GetPackages(string feedIdentifier);
	}
}