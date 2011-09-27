﻿using System;
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
            string json = HttpHelper.Post(string.Format("/templates/{0}.json", clientID), null, JavaScriptConvert.SerializeObject(
                new Dictionary<string, object>() { { "Name", name }, { "HtmlPageURL", htmlPageUrl }, { "ZipFileUrl", zipUrl } })
                );
            return JavaScriptConvert.DeserializeObject<string>(json);
        }

        public void Update(string name, string htmlPageUrl, string zipUrl)
        {
            HttpHelper.Put(string.Format("/templates/{0}.json", TemplateID), null, JavaScriptConvert.SerializeObject(
                new Dictionary<string, object>() { { "Name", name }, { "HtmlPageURL", htmlPageUrl }, { "ZipFileUrl", zipUrl } })
                );
        }

        public BasicTemplate Details()
        {
            string json = HttpHelper.Get(string.Format("/templates/{0}.json", TemplateID), null);
            return JavaScriptConvert.DeserializeObject<BasicTemplate>(json);
        }

        public void Delete()
        {
            HttpHelper.Delete(string.Format("/templates/{0}.json", TemplateID), null);
        }
    }
}
