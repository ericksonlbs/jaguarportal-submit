using dotnet_jaguarportal.Interfaces;
using dotnet_jaguarportal.Jaguar2.Models;
using dotnet_jaguarportal.JaguarPortal.Interfaces;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace dotnet_jaguarportal.JaguarPortal.Services
{
    internal class JaguarPortalService : IJaguarPortalService
    {
        private readonly swaggerClient _client;
        private readonly IJaguarPortalConverter<Jaguar2Model> converterXML;
        private readonly CommandLineParameters? parameters;

        /// <summary>
        /// Jaguar Portal Service
        /// </summary>
        /// <param name="httpClientFactory">httpClientFactory to connection with Jaguar Portal</param>
        /// <param name="converterXML">Converter</param>
        /// <param name="parameters">Command Line Parameters</param>
        /// <exception cref="ArgumentNullException"></exception>
        public JaguarPortalService(IHttpClientFactory httpClientFactory,
                                    IJaguarPortalConverter<Jaguar2Model> converterXML,
                                    CommandLineParameters parameters)
        {
            this.converterXML = converterXML ?? throw new ArgumentNullException(nameof(converterXML));
            this.parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));

            HttpClient httpClient = httpClientFactory.CreateClient();

            if (parameters.HostUrl != null)
                httpClient.BaseAddress = new Uri(parameters.HostUrl);

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
                    using (XmlReader xmlReader = XmlReader.Create(item))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Jaguar2Model));

                        Jaguar2Model? obj = serializer.Deserialize(xmlReader) as Jaguar2Model;


                        if (obj != null)
                        {
                            var analysis = converterXML.Convert(obj, parameters.ProjectKey, parameters.Repository, parameters.Provider,
                                parameters.PullRequestNumber, parameters.PullRequestBranch, parameters.PullRequestBase);

                            var response = await _client.AnalyzesAsync(analysis.Item1);

                            if (parameters.HostUrl == null)
                                return;

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
        }


        private void Notice(string url, Jaguar2Model obj)
        {
            List<KeyValuePair<decimal, string>> notices = new List<KeyValuePair<decimal, string>>();
            Console.WriteLine($"::notice title=Jaguar Portal Analysis::{url}");

            if (obj.package != null)
            {
                foreach (var package in obj.package)
                {
                    if (package.sourcefile != null)
                    {
                        foreach (var sourceFile in package.sourcefile)
                        {
                            if (sourceFile.line != null)
                            {
                                foreach (reportPackageSourcefileLine line in sourceFile.line)
                                {
                                    string notice = $"::notice title={line.susp:0.0000} - {package?.name}/{sourceFile.name}.java - Line: {line.nr} (SBFL RANKING)::{parameters?.LocalPath}/{sourceFile.name}.java CEF:{line.cef} CEP:{line.cep} CNF:{line.cnf} CNP:{line.cnp}";
                                    notices.Add(new KeyValuePair<decimal, string>(line.susp, notice));
                                }
                            }
                        }
                    }
                }
            }

            foreach (var notice in notices.OrderByDescending(x => x.Key))
                Console.WriteLine(notice.Value);
        }
    }
}
