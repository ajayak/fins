namespace FINS.Core.Configuration
{
    public class GeneralSettings
    {
        public string SiteBaseUrl { get; set; }
        public string DefaultTimeZone { get; set; }
    }

    public class DatabaseSettings
    {
        public string ConnectionString { get; set; }
    }

    public class EmailSettings
    {
        public string EmailFolder { get; set; }
    }

    public class SampleDataSettings
    {
        public string DefaultAdminUsername { get; set; }
        public string DefaultAdminPassword { get; set; }
        public string DefaultUsername { get; set; }
        public string DefaultOrganizationUsername { get; set; }
        public string DefaultFromEmailAddress { get; set; }
        public string DefaultFromDisplayName { get; set; }
        public string InsertSampleData { get; set; }
    }

    public class TwitterAuthenticationSettings
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string OAuthToken { get; set; }
        public string OAuthSecret { get; set; }
    }

    public class TwilioSettings
    {
        public string Sid { get; set; }
        public string Token { get; set; }
        public string PhoneNo { get; set; }
    }
}
