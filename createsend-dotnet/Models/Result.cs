using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections;

namespace createsend_dotnet
{
    public class ErrorResult
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public object ResultData { get; set; }
    }

    public class ApiKeyResult
    {
        public string ApiKey { get; set; }
    }

    public class SystemDateResult
    {
        public string SystemDate { get; set; }
    }
}
