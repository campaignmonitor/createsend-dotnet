# createsend-dotnet

A .NET library which implements the complete functionality of v3 of the CreateSend API.

This library is supported on .NET 2, 3.5, and 4. You will find solution files which target version 2 (createsend-dotnet.net20.sln), version 3.5 (createsend-dotnet.net35.sln), and version 4 (createsend-dotnet.sln) of .NET runtime.

## Installation

Using NuGet (recommended):

1. Run `Install-Package campaignmonitor-api` from the Package Manager Console in Visual Studio. See the [NuGet documentation](http://nuget.codeplex.com/documentation) for further details.
2. Edit the following element which is created in the appSettings section of your configuration file (replacing your_api_key with your actual API Key):

        <add key="api_key" value="your_api_key" />

If you don't want to use NuGet:

1. Open the solution file which targets your preferred version of the runtime. 
2. Build the solution in Release mode. You will see a sub-directory in the `createsend-dotnet/bin/Release directory`, which contains the assemblies targeting your preferred version of the runtime. 
3. Add the resulting assemblies as references in your project. 
4. Add the following element to the appSettings section of your project's configuration file (replacing your_api_key with your actual API Key):

        <add key="api_key" value="your_api_key" />

## Example

Example config file:

    <?xml version="1.0"?>
    <configuration>
      <appSettings>
        <add key="api_key" value="98wqdu9qw8ud98quwd9q8wud98uq98wu"/>
      </appSettings>
      <startup><supportedRuntime version="v2.0.50727"/></startup>
    </configuration>

Example console app:
    
    using System;
    using System.Collections.Generic;
    using System.Text;
    using createsend_dotnet;

    namespace dotnet_api_client
    {
        class Program
        {
            static void Main(string[] args)
            {
                IEnumerable<BasicClient> clients = General.Clients();
                foreach (BasicClient c in clients)
                    Console.WriteLine(string.Format("ID: {0}; Name: {1}", c.ClientID, c.Name));
                Console.ReadLine();
            }
        }
    }
