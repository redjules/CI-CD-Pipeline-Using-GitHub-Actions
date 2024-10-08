using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace GitHubActionsDemo.Api.Integration.Tests;

public class Startup
{
    public void ConfigureHost(IHostBuilder hostBuilder)
    {
        var config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        hostBuilder.ConfigureHostConfiguration(builder => builder.AddConfiguration(config));
    }
}