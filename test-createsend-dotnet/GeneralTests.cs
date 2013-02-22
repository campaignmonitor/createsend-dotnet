using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using createsend_dotnet;
using NUnit.Framework;
using Shouldly;

namespace test_createsend_dotnet
{
    [TestFixture]
    public class GeneralTests
    {
        [Test]
        public void AuthorizeUrl_WithoutState_Test()
        {
            var clientID = 8998879;
            var clientSecret = "iou0q9wud0q9wd0q9wid0q9iwd0q9wid0q9wdqwd";
            var redirectUri = "http://example.com/auth";
            var scope = "ViewReports,CreateCampaigns,SendCampaigns";

            var url = General.AuthorizeUrl(clientID, clientSecret, redirectUri, scope);

            url.ShouldBe("https://api.createsend.com/oauth?client_id=8998879&client_secret=iou0q9wud0q9wd0q9wid0q9iwd0q9wid0q9wdqwd&redirect_uri=http%3a%2f%2fexample.com%2fauth&scope=ViewReports%2cCreateCampaigns%2cSendCampaigns");
        }

        [Test]
        public void AuthorizeUrl_WithState_Test()
        {
            var clientID = 8998879;
            var clientSecret = "iou0q9wud0q9wd0q9wid0q9iwd0q9wid0q9wdqwd";
            var redirectUri = "http://example.com/auth";
            var scope = "ViewReports,CreateCampaigns,SendCampaigns";
            var state = "89879287";

            var url = General.AuthorizeUrl(clientID, clientSecret, redirectUri, scope, state);

            url.ShouldBe("https://api.createsend.com/oauth?client_id=8998879&client_secret=iou0q9wud0q9wd0q9wid0q9iwd0q9wid0q9wdqwd&redirect_uri=http%3a%2f%2fexample.com%2fauth&scope=ViewReports%2cCreateCampaigns%2cSendCampaigns&state=89879287");
        }
    }
}
