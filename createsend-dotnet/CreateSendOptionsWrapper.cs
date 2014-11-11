using System;

namespace createsend_dotnet
{
    public class CreateSendOptionsWrapper : ICreateSendOptions
    {
        public string BaseUri
        {
            get { return CreateSendOptions.BaseUri; }
            set { CreateSendOptions.BaseUri = value; }
        }

        public string BaseOAuthUri
        {
            get { return CreateSendOptions.BaseOAuthUri; }
            set { CreateSendOptions.BaseOAuthUri = value; }
        }

        public string VersionNumber
        {
            get { return CreateSendOptions.VersionNumber; }
        }
    }
}
