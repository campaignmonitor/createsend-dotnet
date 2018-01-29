using System;
#if !NETSTANDARD2_0
using System.Configuration;
#endif

namespace createsend_dotnet
{
    public static class CreateSendOptions
    {
        static string base_uri;
        static string base_oauth_uri;

#if NETSTANDARD2_0

        public static ICreateSendOptions Options = null;
#endif
        static CreateSendOptions()
        {

#if !NETSTANDARD2_0 
            base_uri = string.IsNullOrEmpty(
                ConfigurationManager.AppSettings["base_uri"]) ?
                "https://api.createsend.com/api/v3.1" :
                ConfigurationManager.AppSettings["base_uri"];
            base_oauth_uri = string.IsNullOrEmpty(
                ConfigurationManager.AppSettings["base_oauth_uri"]) ?
                "https://api.createsend.com/oauth" :
                ConfigurationManager.AppSettings["base_oauth_uri"];
#endif
#if NETSTANDARD2_0
            base_uri = Options != null ? Options.BaseUri : "https://api.createsend.com/api/v3.1";
            base_oauth_uri = Options != null ? Options.BaseOAuthUri : "https://api.createsend.com/oauth";
#endif
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
                return "4.2.2";
            }
        }
    }
}
