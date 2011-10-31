namespace Mojito.Tests
{
    using System;
    using Mojito.Domain;
    using NUnit.Framework;

    [TestFixture]
    public class FeedTests
    {
        [Test]
        public void Can_Create_New_Feed() {
            var id = Guid.NewGuid();
            var feedName = "Test_" + id;

            var newFeed = new Feed {
                Name = feedName, 
                IsPublic = true, 
                Description = "Test Feed"
            };

            var repository = new FeedRepository();
            repository.Create(newFeed);

            var foundFeed = repository.Fetch(feedName);
            Assert.AreEqual(foundFeed.Name, newFeed.Name);
        }
    }
}
