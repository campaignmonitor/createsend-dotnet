using System.Collections.Generic;

namespace createsend_dotnet
{
    public class Template : CreateSendBase
    {
        public string TemplateID { get; set; }

        public Template(AuthenticationDetails auth, string templateID)
            : base(auth)
        {
            TemplateID = templateID;
        }

        public static string Create(AuthenticationDetails auth,
            string clientID, string name, string htmlPageUrl, string zipUrl)
        {
            return HttpHelper.Post<Dictionary<string, object>, string>(
                auth, string.Format("/templates/{0}.json", clientID), null,
                new Dictionary<string, object>() 
                { 
                    { "Name", name }, 
                    { "HtmlPageURL", htmlPageUrl }, 
                    { "ZipFileUrl", zipUrl }
                });
        }

        public void Update(string name, string htmlPageUrl, string zipUrl)
        {
            HttpPut<Dictionary<string, object>, string>(
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
            return HttpGet<BasicTemplate>(
                string.Format("/templates/{0}.json", TemplateID), null);
        }

        public void Delete()
        {
            HttpDelete(
                string.Format("/templates/{0}.json", TemplateID), null);
        }
    }
}
