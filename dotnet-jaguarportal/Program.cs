using CommandLine;
using dotnet_jaguarportal;
using dotnet_jaguarportal.JaguarPortal.Interfaces;
using dotnet_jaguarportal.JaguarPortal.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace dotnet_jaguarportal
{
    internal class App
    {
        private readonly ILogger<App> _logger;
        private readonly IJaguarPortalService _service;
        private readonly ParserResult<CommandLineParameters> _parserResult;

        public App(ILogger<App> logger, IJaguarPortalService service, ParserResult<CommandLineParameters> parserResult)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service;
            _parserResult = parserResult;
        }

        public async Task Run()
        {
            if (_parserResult.Errors.Count() == 0)
            {
                await _service.SendAnalisysControlFlow();
            }
        }
    }
}
//public class Program
//{
//    public static void Main(string[] args)
//    {

//        //setup our DI
//        var serviceProvider = new ServiceCollection()
//            .AddSingleton(o =>
//            {
//                return Parser.Default.ParseArguments<OptionsCommandLine>(args)
//                                     .WithParsed(o => { }).Value;
//            })
//            .AddScoped<IJaguarPortalService, JaguarPortalService>()
//            .AddHttpClient()
//            .BuildServiceProvider();



//        var service = serviceProvider.GetService<IJaguarPortalService>();

//        //services.Configure<MongoDatabaseSettings>(Configuration.GetSection("MongoDatabaseSettings"));

//        service.SendAnalisysControlFlow(new AnalysisControlFlowNewModel()).GetAwaiter().GetResult();
//    }
//}
