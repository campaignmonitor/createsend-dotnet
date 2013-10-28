# createsend-dotnet history

## v3.1.1 - 28 Oct, 2013

* Removed Newtonsoft dll's from the published nuget package. 

## v3.1.0 - 16 Apr, 2013

* Added support for [single sign on](http://www.campaignmonitor.com/api/account/#single_sign_on) which allows initiation of external login sessions to Campaign Monitor.

## v3.0.0 - 25 Mar, 2013

* Added support for authenticating using OAuth. See the [README](README.md#authenticating) for full usage instructions.
* Refactored authentication so that it is done at the instance level. This introduces some breaking changes, which are clearly explained below.
  * Authentication using an API key is no longer supported using the `api_key` config.

      So if you _previously_ entered an API key into a your `app.config` file (or similar) as follows:

      ```xml
      <configuration>
          <appSettings>
              <add key="api_key" value="your_api_key" />
          </appSettings>
      </configuration>
      ```

      If you want to authenticate with an API key, you should _now_ authenticate at the instance level. For example, as follows:

      ```csharp
      using System;
      using System.Collections.Generic;
      using createsend_dotnet;

      namespace dotnet_api_client
      {
          class Program
          {
              static void Main(string[] args)
              {
                  AuthenticationDetails auth = new ApiKeyAuthenticationDetails(
                      "your api key");
                  var general = new General(auth);
                  var clients = general.Clients();
              }
          }
      }
      ```

  * Instances of classes which inherit from `createsend_dotnet.CreateSendBase` are now _always_ created by passing an `AuthenticationDetails` object as the first argument. This may be either an instance of `OAuthAuthenticationDetails`, or `ApiKeyAuthenticationDetails`.

      So for example, when you _previously_ would have set your API key using the `api_key` config setting and then instantiated `createsend_dotnet.Client` instances like so:

      ```csharp
      var cl = new Client("your client id");
      ```

      You would _now_ do this:

      ```csharp
      AuthenticationDetails auth = new ApiKeyAuthenticationDetails("your api key");
      var cl = new Client(auth, "your client id");
      ```

  * Any of the static methods on classes which inherit from `createsend_dotnet.CreateSendBase` are now _always_ called by passing an `AuthenticationDetails` object as the first argument.

      So for example, when you _previously_ would have set your API key using the `api_key` config setting and then called `createsend_dotnet.List.Create()` like so:

      ```csharp
      var newListID = List.Create("Client ID", "My List", 
          "http://example.com/unsubscribe", false, "",
          UnsubscribeSetting.AllClientLists);
      ```

      You _now_ call `createsend_dotnet.List.Create()` like so:

      ```csharp
      AuthenticationDetails auth = new ApiKeyAuthenticationDetails(
          "your api key");
      var newListID = List.Create(auth, "Client ID", "My List", 
          "http://example.com/unsubscribe", false, "",
          UnsubscribeSetting.AllClientLists);
      ```

## v2.6.0 - 11 Dec, 2012

* Added support for including from name, from email, and reply to email in
drafts, scheduled, and sent campaigns.
* Added support for campaign text version urls.
* Added support for transferring credits to/from a client.
* Added support for getting account billing details as well as client credits.
* Made all date fields optional when getting paged results.

## v2.5.0 - 12 Nov, 2012

* Switched to use https by default (can be overridden in web/app.config).
* Added support for 'send immediate' campaigns. 

## v2.4.0 - 5 Nov, 2012

* Added createsend_dotnet.Campaign.EmailClientUsage().
* Added support for ReadsEmailWith field on subscriber objects.
* Added support for retrieving unconfirmed subscribers for a list.
* Added support for suppressing email addresses.
* Added support for retrieving spam complaints for a campaign, as well as
adding SpamComplaints field to campaign summary output.
* Introduced createsend_dotnet.List.UpdateCustomFieldOptions() method to
replace poorly named createsend_dotnet.List.UpdateCustomFields() method, which
has been marked as obsolete.
* Added VisibleInPreferenceCenter field to custom field output.
* Added support for setting preference center visibility when creating custom
fields.
* Added the ability to update a custom field name and preference visibility.
* Added documentation explaining that textUrl may be provided as null or as
an empty string when creating a campaign.

## v2.3.0 - 10 Oct, 2012

* Added support for creating campaigns from templates.
* Added support for unsuppressing an email address.

## v2.2.1 - 2 Oct, 2012

* Fixed #21. Added CreateSendCredentials class as a replacement for
System.Net.NetworkCredential.

## v2.2.0 - 17 Sep, 2012

* Added WorldviewURL field to campaign summary response.
* Added Latitude, Longitude, City, Region, CountryCode, and CountryName fields
to campaign opens and clicks responses.

## v2.1.0 - 30 Aug, 2012

* Added support for basic / unlimited pricing.

## v2.0.0 - 22 Aug, 2012

* Added support for UnsubscribeSetting field when creating, updating and
getting list details.
* Added support for AddUnsubscribesToSuppList and ScrubActiveWithSuppList
fields when updating a list.
* Removed redundant DateCreated field from ListDetail class.
* Added Samples.ListSamples.GetDetails().
* Added Samples.ListSamples.Update().
* Added createsend_dotnet.Client.ListsForEmail() to allow consumers to find all
client lists to which a subscriber with a specific email address belongs.
* Removed obsolete methods and therefore disallowed calls to be made in a
deprecated manner.

## v1.2.2 - 12 Jul, 2012

* Updated README to recommend that people use NuGet to install.
* Re-release again, ensuring assemblies for all three versions of the .NET
runtime are packaged.

## v1.2.1 - 12 Jul, 2012

* Fixed #18. Last NuGet release wasn't based on a good build.

## v1.2.0 - 11 Jul, 2012

* Added support for APU URL to be modified after construction.
* Added support for specifying whether subscription-based autoresponders
should be restarted when adding or updating subscribers.
* Added support for team management.

## 1.1.0 - 11 Jan, 2012

* Added missing Subscriber.GetHistory method.

## 1.0.16 - 31 Oct, 2011

* Initial release which supports current Campaign Monitor API.
