# createsend-dotnet

A .NET library which implements the complete functionality of v3 of the CreateSend API.

As well as the source code for the library, we provide a single merged assembly (createsend-dotnet.dll) as a download, which can be used directly as a reference in .NET projects, if you do not want to worry about building the solution yourself.

## Installation

The easiest way of getting up and running:

1. Add the createsend-dotnet.dll assembly as a reference in your project
2. Add the following element to the appSettings section of your configuration file (replacing your_api_key with your actual API Key)

        <add key="api_key" value="your_api_key" />

Using NuGet:

1. Install-Package campaignmonitor-api
2. Edit the following element which is created in the appSettings section of your configuration file (replacing your_api_key with your actual API Key):

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
                List<BasicClient> clients = (List<BasicClient>)General.Clients();
                foreach (BasicClient c in clients)
                    Console.WriteLine(string.Format("ID: {0}; Name: {1}", c.ClientID, c.Name));
                Console.ReadLine();
            }
        }
    }
