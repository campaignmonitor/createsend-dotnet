using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace createsend_dotnet
{
    public class General
    {
        public static string ApiKey(string siteUrl, string username, string password)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("SiteUrl", siteUrl);

            HttpHelper.OverrideAuthenticationCredentials(username, password);
            ApiKeyResult result = HttpHelper.Get<ApiKeyResult>("/apikey.json", queryArguments);
            //reset to default authentication
            HttpHelper.OverrideAuthenticationCredentials(CreateSendOptions.ApiKey, "x");

            return result.ApiKey;
        }

        public static DateTime SystemDate()
        {
            return HttpHelper.Get<SystemDateResult>("/systemdate.json", null).SystemDate;
        }

        public static IEnumerable<string> Countries()
        {
            return HttpHelper.Get<string[]>("/countries.json", null);
        }

        public static IEnumerable<string> Timezones()
        {
            return HttpHelper.Get<string[]>("/timezones.json", null);
        }

        public static IEnumerable<BasicClient> Clients()
        {
            return HttpHelper.Get<Clients>("/clients.json", null);
        }
    }
}
