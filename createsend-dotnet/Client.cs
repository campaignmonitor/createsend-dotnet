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
    }
}
