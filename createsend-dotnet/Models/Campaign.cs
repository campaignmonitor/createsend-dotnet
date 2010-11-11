using System;
using System.Collections.Generic;
using System.Text;

namespace createsend_dotnet
{
    public class CampaignDetail
    {
        public string CampaignID { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string SentDate { get; set; }
        public int TotalRecipients { get; set; }
        public string WebVersionURL { get; set; }
    }

    public class DraftDetail
    {
        public string CampaignID { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string DateCreated { get; set; }
        public string PreviewURL { get; set; }
    }
}
