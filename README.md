# Download the Data! Xamarin.Forms and the Internet

What's a mobile app without data? Nothing but a blank screen! And where is all the data? The internet! But oh no - what about when the device goes offline, how do you keep on displaying the downloaded data? This session has you covered. Find out how to grab some data from the internet, great techniques to organize it visually, and keep the app running when it goes offline.

## Installing the demos

You can run all the demos yourself - from an emulator or a real device.

You'll need the following:

* [An Azure subscription (click here for a free one!)](https://azure.microsoft.com/free/?WT.mc_id=mobile-0000-masoucou)
* [Xamarin - click here to install it](https://docs.microsoft.com/xamarin/get-started/installation/?pivots=macos&WT.mc_id=mobile-0000-masoucou)
* [Azure Data Studio - click here to get it](https://docs.microsoft.com/sql/azure-data-studio/download-azure-data-studio?view=sql-server-ver15&WT.mc_id=mobile-0000-masoucou)

### Deploy to Azure

Once you have everything downloaded and installed you'll need to deploy the Azure resources. Click on the button below to do that.

[![Deploy to Azure](https://aka.ms/deploytoazurebutton)](https://portal.azure.com/?WT.mc_id=mobile-0000-masoucou#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fcodemillmatt%2Fmobile-apps-data%2Fmaster%2Fazuredeploy.json)

During the deploy process you'll be asked a couple of questions. They should be explanatory, and you can hover over the _i_ to get more info on it. But one important one is the `Prefix`. The deploy script will prefix all of the Azure resources it creates with the value you put into this field.

> Remember the value of **Sql User Password** - you will need that below.

Another important property is the `RepoUrl` and `Branch`. That's where the deployment script will grab the code to deploy the web API from. You can leave that as-is.

### Configure SQL Server

You'll need to do 2 steps to create the database schema on the SQL service.

1. Allow your local IP address to access it. In the Azure portal, open up the `SQL Server` resource that was created. (If the prefix you specified was _abcd_ then the name of the SQL Server will be _abcdfitappsqlserver_).
    1. Click `Show firewall settings` from the options just underneath the toolbar. (It's right under the server admin username).
    1. Then click `Add client IP` and save it. This will allow the machine you're currently using to access the SQL Server so you can create the database's schema.
1. To create the database schema, open Azure Data Studio.
    1. Then open the all of the files found in the `sql` directory of this repo.
    1. In `01-change-tracking-setup.sql` make sure you enter the _Sql User Password_ you created during the deployment step.
    1. Run all of the scripts found in the `sql` directory, in numerical order.

### Setup the Xamarin project

#### Copy the Web API URl

Once everything has been deployed to Azure and you have installed Xamarin, you will need to copy the Web API's URL into your Xamarin project.

From the Azure portal - visit the `App Service` that was created. (If the prefix you entered was `abcd` then the web app's name will be `abcdfitappweb`).

Once that's open - copy the value found in the `URL` field.

#### Setup the Web API within Xamarin

Each of the demos is arranged in a different folder. So you will need to update the `src\demo-1\FitApp\FitApp\Constants.cs` file for each demo.

Within the constants file change:

```language-csharp
public static readonly string WebServerBaseUrl = "ENTER YOUR WEBSITE'S URL HERE";
```

To be your Web API's URL.

## The Demos

### Downloading Data

During the session you learned about how Xamarin allows you to go out to the internet and download data using C# and .NET - just as if you were downloading data via any other .NET application.

Even though iOS and Android access the Internet in very different ways - Xamarin allows you to download data the .NET way.

```language-csharp
var request = new HttpRequestMessage(HttpMethod.Post, syncRequestUrl);

// syncRequest is just an object specific to the web call we're making
request.Content = new StringContent(JsonConvert.SerializeObject(syncRequest), Encoding.UTF8, "application/json");

var response = await client.SendAsync(request);

// pull out the info
var syncResultJson = await response.Content.ReadAsStringAsync();

// DataSyncResult is the object we're expecting the web service to return
var syncResult = JsonConvert.DeserializeObject<DataSyncResult>(syncResultJson);
```

That's it! Your mobile app can now download data from the web!

Learn more about making [web requests with the documentation here](https://docs.microsoft.com/xamarin/xamarin-forms/data-cloud/web-services/rest?WT.mc_id=mobile-0000-masoucou).

### Local Data

Sometimes though you want to save data locally to the device. This could be because you don't want to make round trips to the web. Or the user may be offline. Or you don't want to download a ton of data.

There are numerous ways to save data on device. You can do so in [files](https://docs.microsoft.com/xamarin/xamarin-forms/data-cloud/data/files?tabs=windows&WT.mc_id=mobile-0000-masoucou), via [on-device databases](https://docs.microsoft.com/xamarin/xamarin-forms/data-cloud/data/databases?WT.mc_id=mobile-0000-masoucou), or by [preferences](https://docs.microsoft.com/xamarin/essentials/preferences?WT.mc_id=mobile-0000-masoucou).

We looked at preferences and a community powered tool called MonkeyCache that wraps up SQLite database calls in a nice, easy to use API.

### Data Synchronization

What happens if you have data on multiple devices and one device has been offline for a very long time - all the while the other device has been making changes to that data - updating, inserting, and deleting.

How do you get the data down from the internet with the least amount of work possible?

Well, if you're using [Azure SQL Server](https://docs.microsoft.com/azure/azure-sql/database/?WT.mc_id=mobile-0000-masoucou) - you can use a function called Change Table which does all of the work for you.

And the best part is - there's no work in it for you. Your mobile app pretty much stays as is. Check out demo 3 for all the info.

### Displaying the Data

Demo 4 was all about making sure the data looks great for your users.

And there are a ton of community contributions that allow you to do that - with little work on your end.

We took a look at a couple of them.

* [Snppts UI website](https://snppts.dev/)
* [Pancakeview](https://github.com/sthewissen/Xamarin.Forms.PancakeView)
* [Material Frame](https://github.com/roubachof/Sharpnado.MaterialFrame)

Each of those are awesome in their own right - but combined together you can spin up a beautiful app in no time at all.

## Summing Up

Wrapping all of this up - downloading data with a Xamarin app is the same as you would do with any other .NET app - even though the platforms are completely different.

Sometimes you need to store data locally - and MonkeyCache is a great way to use SQLite to do so, with an easy to use API.

Check out Azure SQL and its change tracking feature to let you sync changes super duper fast.

And the community contributions can make your app look better than ever!