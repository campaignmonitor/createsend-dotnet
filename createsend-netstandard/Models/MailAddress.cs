using System;
using System.Collections.Generic;
using System.Text;

namespace createsend_netstandard.Models
{
    public class MailAddress
    {
        private string displayName;

        private string emailAddress;

        public MailAddress(string emailAddress, string displayName)
        {
            this.emailAddress = emailAddress;
            this.displayName = displayName;
        }

        public string Address { get { return this.emailAddress; } }

        public string DisplayName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.displayName))
                {
                    return this.emailAddress;
                }
                return this.displayName;
            }
        }
    }
}