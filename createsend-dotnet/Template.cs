using System.Collections.Generic;

namespace createsend_dotnet
{
    public class Template
    {
        public string ApiKey { get; set; }

        private CampMonCredentials AuthCredentials
        {
            get { return new CampMonCredentials(ApiKey != null ? ApiKey : CreateSendOptions.ApiKey, "x"); }
        }

        public string TemplateID { get; set; }

        public Template(string templateID)
        {
            TemplateID = templateID;
        }

        public static string Create(string apiKey, string clientID, string name, string htmlPageUrl, string zipUrl)
        {
            return HttpHelper.Post<Dictionary<string, object>, string>(
                new CampMonCredentials(apiKey, "x"), 
                string.Format("/templates/{0}.json", clientID), null,
                new Dictionary<string, object>() 
                { 
                    { "Name", name }, 
                    { "HtmlPageURL", htmlPageUrl }, 
                    { "ZipFileUrl", zipUrl }
                });
        }

        public static string Create(string clientID, string name, string htmlPageUrl, string zipUrl)
        {
            return Create(CreateSendOptions.ApiKey, clientID, name, htmlPageUrl, zipUrl);
        }

        public void Update(string name, string htmlPageUrl, string zipUrl)
        {
            HttpHelper.Put<Dictionary<string, object>, string>(
                AuthCredentials, 
                string.Format("/templates/{0}.json", TemplateID), null,
                new Dictionary<string, object>() 
                { 
                    { "Name", name }, 
                    { "HtmlPageURL", htmlPageUrl }, 
                    { "ZipFileUrl", zipUrl }
                });
        }

        public BasicTemplate Details()
        {
            return HttpHelper.Get<BasicTemplate>(AuthCredentials, string.Format("/templates/{0}.json", TemplateID), null);
        }

        public void Delete()
        {
            HttpHelper.Delete(AuthCredentials, string.Format("/templates/{0}.json", TemplateID), null);
        }
    }
}
