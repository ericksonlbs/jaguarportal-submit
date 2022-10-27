using CommandLine;
using dotnet_jaguarportal.JaguarPortal.Interfaces;

namespace dotnet_jaguarportal
{
    internal class App
    {
        private readonly IJaguarPortalService _service;
        private readonly ParserResult<CommandLineParameters> _parserResult;

        public App(IJaguarPortalService service, ParserResult<CommandLineParameters> parserResult)
        {
            _service = service;
            _parserResult = parserResult;
        }

        public async Task Run()
        {
            if (!_parserResult.Errors.Any())
                await _service.SendAnalisysControlFlow();
        }
    }
}