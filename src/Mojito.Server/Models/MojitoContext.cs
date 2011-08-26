namespace Mojito.Server.Models {
    using System.Collections.Generic;
    using System.Linq;

    public class MojitoContext {
        public IQueryable<FeedServiceModel> Feeds
        {
            get { return new List<FeedServiceModel> { new FeedServiceModel { Name = "github", 
                Description = "Packages that are hosted out on github."} }.AsQueryable(); }
        }
    }
}
