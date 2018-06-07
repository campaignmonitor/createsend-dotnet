using System;
using System.Configuration;

namespace createsend_dotnet
{
    public static class CreateSendOptions
    {
        static string base_uri;
        static string base_oauth_uri;

        static CreateSendOptions()
        {
            base_uri = string.IsNullOrEmpty(
                ConfigurationManager.AppSettings["base_uri"]) ?
                "https://api.createsend.com/api/v3.2" :
                ConfigurationManager.AppSettings["base_uri"];
            base_oauth_uri = string.IsNullOrEmpty(
                ConfigurationManager.AppSettings["base_oauth_uri"]) ?
                "https://api.createsend.com/oauth" :
                ConfigurationManager.AppSettings["base_oauth_uri"];
        }

        public static string BaseUri
        {
            get { return base_uri; }
            set { base_uri = value; }
        }

        public static string BaseOAuthUri
        {
            get { return base_oauth_uri; }
            set { base_oauth_uri = value; }
        }

        public static string VersionNumber
        {
            get
            {
                return "5.0.1";
            }
        }
    }
}
