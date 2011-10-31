namespace Mojito.Server.Models.Input {
    using System;
    using System.ComponentModel.DataAnnotations;

    [Serializable]
    public class CreateNewFeed {
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
