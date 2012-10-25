# createsend-dotnet history

## v2.3.0 - 10 Oct, 2012   (c268b312)

* Added support for creating campaigns from templates.
* Added support for unsuppressing an email address.

## v2.2.1 - 2 Oct, 2012   (36a9c322)

* Fixed #21. Added CreateSendCredentials class as a replacement for
System.Net.NetworkCredential.

## v2.2.0 - 17 Sep, 2012   (ddd1c98e)

* Added WorldviewURL field to campaign summary response.
* Added Latitude, Longitude, City, Region, CountryCode, and CountryName fields
to campaign opens and clicks responses.

## v2.1.0 - 30 Aug, 2012   (68b397f5)

* Added support for basic / unlimited pricing.

## v2.0.0 - 22 Aug, 2012   (a950bfd2)

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

## v1.2.2 - 12 Jul, 2012   (6be0f551)

* Updated README to recommend that people use NuGet to install.
* Re-release again, ensuring assemblies for all three versions of the .NET
runtime are packaged.

## v1.2.1 - 12 Jul, 2012   (10e51ef4)

* Fixed #18. Last NuGet release wasn't based on a good build.

## v1.2.0 - 11 Jul, 2012   (1a548295)

* Added support for APU URL to be modified after construction.
* Added support for specifying whether subscription-based autoresponders
should be restarted when adding or updating subscribers.
* Added support for team management.

## 1.1.0 - 11 Jan, 2012   (a9453924)

* Added missing Subscriber.GetHistory method.

## 1.0.16 - 31 Oct, 2011   (2580c245)

* Initial release which supports current Campaign Monitor API.
