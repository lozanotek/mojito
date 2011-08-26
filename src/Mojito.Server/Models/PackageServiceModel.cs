namespace Mojito.Server.Models
{
    using System;
    using System.Data.Services.Common;

    [DataServiceKey("Id", "Version")]
    [EntityPropertyMapping("Id", SyndicationItemProperty.Title, SyndicationTextContentKind.Plaintext, keepInContent: false)]
    [EntityPropertyMapping("Authors", SyndicationItemProperty.AuthorName, SyndicationTextContentKind.Plaintext, keepInContent: false)]
    [EntityPropertyMapping("LastUpdated", SyndicationItemProperty.Updated, SyndicationTextContentKind.Plaintext, keepInContent: false)]
    [EntityPropertyMapping("Summary", SyndicationItemProperty.Summary, SyndicationTextContentKind.Plaintext, keepInContent: false)]
    [HasStream]
    public class PackageServiceModel
    {
        internal string Path
        {
            get;
            set;
        }

        public string Id
        {
            get;
            set;
        }

        public string Version
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Authors
        {
            get;
            set;
        }

        public string IconUrl
        {
            get;
            set;
        }

        public string LicenseUrl
        {
            get;
            set;
        }

        public string ProjectUrl
        {
            get;
            set;
        }

        public string ReportAbuseUrl
        {
            get;
            set;
        }

        public int DownloadCount
        {
            get;
            set;
        }

        public int VersionDownloadCount
        {
            get;
            set;
        }

        public int RatingsCount
        {
            get;
            set;
        }

        public int VersionRatingsCount
        {
            get;
            set;
        }

        public double Rating
        {
            get;
            set;
        }

        public double VersionRating
        {
            get;
            set;
        }

        public bool RequireLicenseAcceptance
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Summary
        {
            get;
            set;
        }

        public string Language
        {
            get;
            set;
        }

        public DateTime Published
        {
            get;
            set;
        }

        public DateTime LastUpdated
        {
            get;
            set;
        }

        public decimal Price
        {
            get;
            set;
        }

        public string Dependencies
        {
            get;
            set;
        }

        public string PackageHash
        {
            get;
            set;
        }

        public long PackageSize
        {
            get;
            set;
        }

        public string ExternalPackageUri
        {
            get;
            set;
        }

        public string Categories
        {
            get;
            set;
        }

        public string Copyright
        {
            get;
            set;
        }

        public string PackageType
        {
            get;
            set;
        }

        public string Tags
        {
            get;
            set;
        }

        public bool IsLatestVersion
        {
            get;
            set;
        }
    }
}
