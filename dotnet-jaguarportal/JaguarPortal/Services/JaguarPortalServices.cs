using dotnet_jaguarportal.Interfaces;
using dotnet_jaguarportal.Jaguar2.Models;
using dotnet_jaguarportal.JaguarPortal.Interfaces;
using System.Text;
using System.Xml.Serialization;

namespace dotnet_jaguarportal.JaguarPortal.Services
{
    internal class JaguarPortalService : IJaguarPortalService
    {
        private readonly swaggerClient _client;
        private readonly IJaguarPortalConverter<Jaguar2Model> converterXML;
        private readonly CommandLineParameters? parameters;

        public JaguarPortalService(IHttpClientFactory httpClientFactory, IJaguarPortalConverter<Jaguar2Model> converterXML, CommandLineParameters parameters)
        {
            if (parameters == null)
            {
                return;
            }
            this.converterXML = converterXML ?? throw new ArgumentNullException(nameof(converterXML));
            this.parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));

            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(parameters?.HostUrl);
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            if (parameters.ClientId != null && parameters.ClientSecret != null)
                httpClient.GenerateAccessToken(parameters.ClientId, parameters.ClientSecret);

            _client = new swaggerClient(parameters?.HostUrl, httpClient);
        }

        public async Task SendAnalisysControlFlow()
        {
            if (parameters != null &&
                parameters.SBFLPathResult != null &&
                parameters.ProjectKey != null)
            {

                foreach (var item in Directory.GetFiles(parameters.SBFLPathResult, "*.xml", SearchOption.TopDirectoryOnly))
                {
                    XmlSerializer serializer2 = new XmlSerializer(typeof(Jaguar2Model));
                    MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(item)));
                    Jaguar2Model? obj = serializer2.Deserialize(memStream) as Jaguar2Model;
                    if (obj != null)
                    {
                        var analysis = converterXML.Convert(obj, parameters.ProjectKey);

                        var response = await _client.AnalyzesAsync(analysis.Item1);

                        string url = string.Concat(parameters.HostUrl, parameters.HostUrl.EndsWith("/") ? "" : "/", "Analyzes/Detail/", response.Id);

                        Notice(url, obj);
                        try
                        {
                            foreach (var @class in analysis.Item2)
                            {
                                await _client.ClassAsync(response.Id, @class);
                            }
                            await _client.FinalizeAsync(response.Id, new FinalizeAnalysisModel() { Status = StatusFinalize._1 });
                        }
                        catch (Exception ex)
                        {
                            if (response?.Id != null)
                                await _client.FinalizeAsync(response.Id, new FinalizeAnalysisModel() { MessageError = ex.Message, Status = StatusFinalize._0 });
                            throw;
                        }

                    }
                }
            }
        }


        private void Notice(string url, Jaguar2Model obj)
        {
            List<KeyValuePair<decimal, string>> notices = new List<KeyValuePair<decimal, string>>();
            Console.WriteLine($"::notice title=Jaguar Portal Analysis::{url}");

            foreach (var @class in obj.Classes)
            {
                if (@class.Lines != null)
                    foreach (sbflClassLine line in @class?.Lines)
                    {
                        string notice = $"::notice title={line.SuspiciousnessValue:0.0000} - {parameters?.LocalPath}/{@class.name}.java - Line: {line.nr} (SBFL RANKING)::{parameters?.LocalPath}/{@class.name}.java CEF:{line.cef} CEP:{line.cep} CNF:{line.cnf} CNP:{line.cnp}";
                        notices.Add(new KeyValuePair<decimal, string>(line.SuspiciousnessValue, notice));
                    }
            }

            foreach (var notice in notices.OrderByDescending(x => x.Key))
                Console.WriteLine(notice.Value);
        }
    }
}
