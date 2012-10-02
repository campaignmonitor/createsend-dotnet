using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace createsend_dotnet
{
    public class General
    {
        public static string ApiKey(string siteUrl, string username, string password)
        {
            NameValueCollection queryArguments = new NameValueCollection();
            queryArguments.Add("SiteUrl", siteUrl);

            CampMonCredentials credential = new CampMonCredentials(username, password);
            ApiKeyResult result = HttpHelper.Get<ApiKeyResult>(credential, "/apikey.json", queryArguments);

            return result.ApiKey;
        }

        public static DateTime SystemDate(string apiKey)
        {
            CampMonCredentials credential = new CampMonCredentials(apiKey, "x");
            return HttpHelper.Get<SystemDateResult>(credential, "/systemdate.json", null).SystemDate;
        }

        public static DateTime SystemDate()
        {
            return SystemDate(CreateSendOptions.ApiKey);
        }

        public static IEnumerable<string> Countries(string apiKey)
        {
            CampMonCredentials credential = new CampMonCredentials(apiKey, "x");
            return HttpHelper.Get<string[]>(credential, "/countries.json", null);
        }

        public static IEnumerable<string> Countries()
        {
            return Countries(CreateSendOptions.ApiKey);
        }

        public static IEnumerable<string> Timezones(string apiKey)
        {
            CampMonCredentials credential = new CampMonCredentials(apiKey, "x");
            return HttpHelper.Get<string[]>(credential, "/timezones.json", null);
        }

        public static IEnumerable<string> Timezones()
        {
            return Timezones(CreateSendOptions.ApiKey);
        }

        public static IEnumerable<BasicClient> Clients(string apiKey)
        {
            CampMonCredentials credential = new CampMonCredentials(apiKey, "x");
            return HttpHelper.Get<Clients>(credential, "/clients.json", null);
        }

        public static IEnumerable<BasicClient> Clients()
        {
            return Clients(CreateSendOptions.ApiKey);
        }
    }
}
