# Written to be executed on cygwin for now.

desc "Build solutions for release"
task :solutions do
  puts "Building solutions for release..."
  msbuild = "c:/Windows/Microsoft.NET/Framework64/v4.0.30319/msbuild.exe"
  system "#{msbuild} createsend-dotnet.sln /t:Clean,Build /p:Configuration=Release"
  system "#{msbuild} createsend-dotnet.net35.sln /t:Clean,Build /p:Configuration=Release"
  system "#{msbuild} createsend-dotnet.net20.sln /t:Clean,Build /p:Configuration=Release"
end

desc "Build NuGet package"
task :build => :solutions do
  puts "Building NuGet package..."
  system "rm -rf campaignmonitor-api.*.nupkg"
  system "nuget pack createsend-dotnet.nuspec"
end

desc "Build NuGet package and push to NuGet"
task :release => :build do
  # You will need to set your NuGet API key before releasing:
  # $ nuget setapikey {apikey}
  puts "Releasing NuGet package..."
  system "nuget push campaignmonitor-api.*.nupkg"
end

task :default => :build
