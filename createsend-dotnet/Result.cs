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

    [XmlRoot("ArrayOfstring")]
    public class ArrayOfstring : IEnumerable<string>
    {
        private List<string> _countries = new List<string>();

        [XmlIgnore]
        public List<string> Countries
        {
            get { return _countries; }
            set { _countries = value; }
        }

        public void Add(string item)
        {
            _countries.Add(item);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _countries.GetEnumerator();
        }        

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return _countries.GetEnumerator();
        }
    }
}
