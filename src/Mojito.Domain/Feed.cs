namespace Mojito.Domain {
    using System;
    using System.Collections.Generic;
    using System.Data.Services.Common;
    using System.Linq;
    using PetaPoco;

    [Serializable]
    [TableName("Feeds")]
    //[PrimaryKey("Id")]
    public class Feed {
        [Column]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public string Provider { get; set; }
        public bool IsPublic { get; set; }

        // Figure out how to ignore this
        [Ignore]
        public IList<Package> Packages { get; set; } 
    }

    public interface IFeedRepository {
        Feed Fetch(string feedName);
        void Create(Feed newFeed);
        void Delete(string feedName);
        void Update(Feed updatedFeed);

        IList<Feed> All();
    }

    public abstract class RepositoryBase {
        protected virtual string DatabaseName {get { return "mojito.Database"; }}

        protected Database GetDatabase() {
            return GetDatabase(DatabaseName);
        }

        public Database GetDatabase(string connectionName) {
            return new Database(connectionName);
        }
    }

    public class FeedRepository : RepositoryBase, IFeedRepository {
        
        public Feed Fetch(string feedName) {
            var db = GetDatabase();
            return db.SingleOrDefault<Feed>("SELECT * FROM Feeds WHERE [Name] = @0", feedName);
        }

        public void Create(Feed newFeed) {
            if (newFeed == null) {
                throw new ArgumentNullException("newFeed", "Please provide a valid Feed object.");
            }

            if (newFeed.Id == Guid.Empty) {
                newFeed.Id = Guid.NewGuid();
            }

            newFeed.Created = DateTime.Now;
            
            var db = GetDatabase();
            db.Insert(newFeed);
        }

        public void Delete(string feedName) {
            var db = GetDatabase();

            // Delete an article
            db.Delete<Feed>("WHERE [Name]=@0", feedName);
        }

        public void Update(Feed updatedFeed) {
            var db = GetDatabase();
            db.Update(updatedFeed);
        }

        public IList<Feed> All() {
            var db = GetDatabase();
            return db.Fetch<Feed>("SELECT * FROM Feed WHERE IsPublic = 1");
        }
    }
}
