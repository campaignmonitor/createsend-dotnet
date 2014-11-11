using System;

namespace createsend_dotnet
{
    public interface ICreateSendOptions
    {
        string BaseUri { get; set; }
        string BaseOAuthUri { get; set; }
        string VersionNumber { get; }
    }
}
