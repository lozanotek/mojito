namespace Turon.DataServices.Recipes {
	using AutoMapper;
	using Turon.Domain;

	public static class EntityExtensions {
		static EntityExtensions() {
			Mapper.CreateMap<Package, Recipe>();
		}

		public static Package ToPackage(this Recipe recipe) {
			return Mapper.Map<Recipe, Package>(recipe);
		}
	}
}
