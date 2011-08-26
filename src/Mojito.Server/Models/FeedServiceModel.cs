namespace Mojito.Server.Models {
    using System;
    using System.Data.Services.Common;

    [Serializable]
    [DataServiceKey("Id", "Name")]
    [EntityPropertyMapping("Name", SyndicationItemProperty.Title, SyndicationTextContentKind.Plaintext, keepInContent: false)]
    [EntityPropertyMapping("Created", SyndicationItemProperty.Updated, SyndicationTextContentKind.Plaintext, keepInContent: false)]
    [EntityPropertyMapping("Description", SyndicationItemProperty.Summary, SyndicationTextContentKind.Plaintext, keepInContent: false)]    
    public class FeedServiceModel {
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public string Provider { get; set; }
    }
}
