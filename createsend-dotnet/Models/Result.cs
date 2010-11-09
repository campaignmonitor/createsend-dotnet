using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections;

namespace createsend_dotnet
{
    [XmlRoot("Result")]
    public class ErrorResult
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }

    [XmlRoot("Result")]
    public class ApiKeyResult
    {
        public string ApiKey { get; set; }
    }

    [XmlRoot("Result")]
    public class SystemDateResult
    {
        public string SystemDate { get; set; }
    }
}
