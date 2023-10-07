using CommandLine;
using dotnet_jaguarportal;
using dotnet_jaguarportal.GitHub.Services;
using dotnet_jaguarportal.Interfaces;
using dotnet_jaguarportal.JaguarPortal.Interfaces;
using dotnet_jaguarportal.JaguarPortal.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Octokit;

static void ConfigureServices(IServiceCollection services, string[] args)
{
    // configure logging
    services.AddLogging(builder =>
    {
        builder.AddConsole();
        builder.AddDebug();
    });

    // add services:
    services.AddSingleton<ParserResult<CommandLineParameters>>(o =>
     {
         return Parser.Default.ParseArguments<CommandLineParameters>(args)
                              .WithParsed(o => { });
     });

    services.AddSingleton<CommandLineParameters>(x =>
    {
        return x.GetRequiredService<ParserResult<CommandLineParameters>>().Value;
    });

    services.AddSingleton<GitHubClient>(x =>
    {
        CommandLineParameters param = x.GetRequiredService<CommandLineParameters>();

        GitHubClient client = new GitHubClient(new ProductHeaderValue("Jaguar2"));

        if (!string.IsNullOrWhiteSpace(param.BotAccessToken))
        {
            var tokenAuth = new Credentials(param.BotAccessToken);
            client.Credentials = tokenAuth;
        }

        return client;
    });

    services.AddScoped<IJaguarPortalService, JaguarPortalService>();
    services.AddScoped<IGitHubService, GitHubService>();

    services.AddScoped<IJaguarPortalConverter<dotnet_jaguarportal.Jaguar2.Models.Jaguar2Model>, dotnet_jaguarportal.Jaguar2.Services.Jaguar2Converter>();


    services.AddHttpClient();
    // add app
    services.AddTransient<App>();
}

// create service collection
var services = new ServiceCollection();

ConfigureServices(services, args);

// create service provider
using var serviceProvider = services.BuildServiceProvider();

// entry to run app
await serviceProvider.GetRequiredService<App>().Run();