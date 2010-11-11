using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace createsend_dotnet
{
    public class Client
    {
        public static string Create(ClientDetail newClient)
        {
            string json = HttpHelper.Post("/clients.json", null, JavaScriptConvert.SerializeObject(newClient));
            return JavaScriptConvert.DeserializeObject<string>(json);
        }

        public static ClientWithSettings Details(string clientID)
        {
            string json = HttpHelper.Get(string.Format("/clients/{0}.json", clientID), null);
            return JavaScriptConvert.DeserializeObject<ClientWithSettings>(json);
        }

        public static IEnumerable<CampaignDetail> Campaigns(string clientID)
        {
            string json = HttpHelper.Get(string.Format("/clients/{0}/campaigns.json", clientID), null);
            return JavaScriptConvert.DeserializeObject<CampaignDetail[]>(json);
        }

        public static IEnumerable<DraftDetail> Drafts(string clientID)
        {
            string json = HttpHelper.Get(string.Format("/clients/{0}/drafts.json", clientID), null);
            return JavaScriptConvert.DeserializeObject<DraftDetail[]>(json);
        }

        public static IEnumerable<BasicList> Lists(string clientID)
        {
            string json = HttpHelper.Get(string.Format("/clients/{0}/lists.json", clientID), null);
            return JavaScriptConvert.DeserializeObject<BasicList[]>(json);
        }

        public static IEnumerable<BasicSegment> Segments(string clientID)
        {
            string json = HttpHelper.Get(string.Format("/clients/{0}/segments.json", clientID), null);
            return JavaScriptConvert.DeserializeObject<BasicSegment[]>(json);
        }
    }
}
