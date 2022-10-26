using CommandLine;
using dotnet_jaguarportal;
using dotnet_jaguarportal.Jaguar.Interfaces;
using dotnet_jaguarportal.JaguarPortal.Interfaces;
using dotnet_jaguarportal.JaguarPortal.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

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

    services.AddScoped<IJaguarPortalService, JaguarPortalService>();
    services.AddScoped<IJaguarPortalConverter<dotnet_jaguarportal.Jaguar.Models.FlatFaultClassification>,
        dotnet_jaguarportal.Jaguar.Services.JaguarConverter>();
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