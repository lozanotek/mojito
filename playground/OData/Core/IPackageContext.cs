namespace Turon.Core {
	using System.Linq;
	using Turon.Domain;

	public interface IPackageContext {
		IQueryable<Package> Packages { get; }
	}
}
