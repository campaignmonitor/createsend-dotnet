# Releasing createsend-dotnet

## Requirements

- You must have a [NuGet](https://www.nuget.org/) account and must be an owner of the [campaignmonitor-api](https://www.nuget.org/packages/campaignmonitor-api) package.
- You must have the `nuget` command line tool installed.
- The [Rakefile](https://github.com/campaignmonitor/createsend-dotnet/blob/master/Rakefile) used to automate the process of releasing the package requires that you have [Cygwin](http://www.cygwin.com/), [Ruby](http://www.ruby-lang.org/en/), and [Rake](http://rake.rubyforge.org/) installed. If you don't, you'll need to manually following the processes automated by the rake tasks.

## Prepare the release

- Increment version numbers in the following files, ensuring that you use [Semantic Versioning](http://semver.org/):
  * `createsend-dotnet.nuspec`
  * `createsend-dotnet/HttpHelper.cs`
  * `createsend-dotnet/Properties/AssemblyInfo.cs`
- Add an entry to `HISTORY.md` which clearly explains the new release.
- Ensure that all solutions build successfully:

  ```
  rake solutions
  ```

- Commit your changes:

  ```
  git commit -am "Version X.Y.Z"
  ```

- Tag the new version:

  ```
  git tag -a vX.Y.Z -m "Version X.Y.Z"
  ```

- Push your changes to GitHub, including the tag you just created:

  ```
  git push origin master --tags
  ```

## Build the package

```
rake build
```

This builds the package locally to a file named something like `campaignmonitor-api.X.Y.Z.nupkg`. You're now ready to release the package.

## Release the package

Before you upload the package to NuGet, you need to tell NuGet which API key to use for uploading. You can do so like this:

```
nuget setapikey {apikey}
```

You should only need to do this the first time you use upload the package. The API key should be cached locally after that.

Then, release the package:

```
rake release
```

This publishes the package to [NuGet](https://www.nuget.org/packages/campaignmonitor-api). You should see the newly published version of the package there. All done!
