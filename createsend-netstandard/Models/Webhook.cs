using System;
using System.Collections.Generic;
using System.Text;

namespace createsend_dotnet
{
    public class BasicWebhook
    {
        public string WebhookID { get; set; }
        public List<string> Events { get; set; }
        public string Url { get; set; }
        public string Status { get; set; }
        public string PayloadFormat { get; set; }
    }

    public class WebhookTestErrorResult
    {
        public string FailureStatus { get; set; }
        public string FailureResponseMessage { get; set; }
        public string FailureResponseCode { get; set; }
        public string FailureResponse { get; set; }
    }
}
