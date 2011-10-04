using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace createsend_dotnet
{
    public class Template
    {
        public string TemplateID { get; set; }

        public Template(string templateID)
        {
            TemplateID = templateID;
        }

        public static string Create(string clientID, string name, string htmlPageUrl, string zipUrl)
        {
            return HttpHelper.Post<Dictionary<string, object>, string>(
                string.Format("/templates/{0}.json", clientID), null,
                new Dictionary<string, object>() 
                { 
                    { "Name", name }, 
                    { "HtmlPageURL", htmlPageUrl }, 
                    { "ZipFileUrl", zipUrl }
                });
        }

        public void Update(string name, string htmlPageUrl, string zipUrl)
        {
            HttpHelper.Put<Dictionary<string, object>, string>(
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
            return HttpHelper.Get<BasicTemplate>(string.Format("/templates/{0}.json", TemplateID), null);
        }

        public void Delete()
        {
            HttpHelper.Delete(string.Format("/templates/{0}.json", TemplateID), null);
        }
    }
}
