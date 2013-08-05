namespace Turon.Recipes {
	using AutoMapper;
	using Turon.Domain;

	public static class EntityExtensions {
		static EntityExtensions() {
			Mapper.CreateMap<Recipe, Package>();
		}

		public static Package ToPackage(this Recipe recipe) {
			return Mapper.Map<Recipe, Package>(recipe);
		}
	}
}
