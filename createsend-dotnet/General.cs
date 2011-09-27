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
            string json = HttpHelper.Get("/apikey.json", queryArguments);
            //reset to default authentication
            HttpHelper.OverrideAuthenticationCredentials(CreateSendOptions.ApiKey, "x");
            return JsonConvert.DeserializeObject<ApiKeyResult>(json).ApiKey;
        }

        public static DateTime SystemDate()
        {
            string json = HttpHelper.Get("/systemdate.json", null);
            return DateTime.Parse(JsonConvert.DeserializeObject<SystemDateResult>(json).SystemDate);
        }

        public static IEnumerable<string> Countries()
        {
            string json = HttpHelper.Get("/countries.json", null);
            return JsonConvert.DeserializeObject<string[]>(json);
        }

        public static IEnumerable<string> Timezones()
        {
            string json = HttpHelper.Get("/timezones.json", null);
            return JsonConvert.DeserializeObject<string[]>(json);
        }

        public static IEnumerable<BasicClient> Clients()
        {
            string json = HttpHelper.Get("/clients.json", null);
            return JsonConvert.DeserializeObject<Clients>(json);
        }
    }
}
