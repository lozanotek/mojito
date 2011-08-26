namespace Mojito.Domain {
    using System;
    using System.Collections.Generic;
    using System.Data.Services.Common;

    [Serializable]
    [DataServiceKey("Id", "Name")]
    //[EntityPropertyMapping("Packages", SyndicationItemProperty.Summary, SyndicationTextContentKind.Plaintext, false)]//
    public class Feed {
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public string Provider { get; set; }
        
        // Figure out how to ignore this
        // public IList<Package> Packages { get; set; } 
    }
}
