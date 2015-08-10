using System;

namespace createsend_dotnet
{
    public class CustomCreateSendOptions : ICreateSendOptions
    {
        public string BaseUri
        {
            get;
            set;
        }

        public string BaseOAuthUri
        {
            get;
            set;
        }

        public string VersionNumber
        {
            get { return CreateSendOptions.VersionNumber; }
        }
    }
}