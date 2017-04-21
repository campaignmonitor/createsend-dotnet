using System;

namespace createsend_dotnet
{
    public static class CreateSendOptions
    {
        private static string base_oauth_uri;
        private static string base_uri;

        static CreateSendOptions()
        {
            base_uri = "https://api.createsend.com/api/v3.1";
            base_oauth_uri = "https://api.createsend.com/oauth";
        }

        public static string BaseOAuthUri
        {
            get { return base_oauth_uri; }
            set { base_oauth_uri = value; }
        }

        public static string BaseUri
        {
            get { return base_uri; }
            set { base_uri = value; }
        }

        public static string VersionNumber
        {
            get
            {
                return "4.2.2";
            }
        }
    }
}