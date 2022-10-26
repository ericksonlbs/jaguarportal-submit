using dotnet_jaguarportal.Jaguar.Interfaces;
using dotnet_jaguarportal.JaguarPortal.Interfaces;
using dotnet_jaguarportal.JaguarPortal.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace dotnet_jaguarportal.JaguarPortal.Services
{
    internal class JaguarPortalService : IJaguarPortalService
    {
        private readonly swaggerClient _client;
        private readonly Jaguar.Interfaces.IJaguarPortalConverter<Jaguar.Models.FlatFaultClassification> converter;
        private readonly CommandLineParameters parameters;

        public JaguarPortalService(IHttpClientFactory httpClientFactory, IJaguarPortalConverter<Jaguar.Models.FlatFaultClassification> converter, CommandLineParameters parameters)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Add($"X-API-Key", $"{parameters?.ApiKey}");
            _client = new swaggerClient(parameters?.HostUrl, httpClient);
            this.converter = converter;
            this.parameters = parameters;
        }

        public Task SendAnalisysControlFlow()
        {
            foreach (var item in Directory.GetFiles(parameters.SBFLPathResult, "*.xml", SearchOption.TopDirectoryOnly))
            {
                XmlSerializer serializer2 = new XmlSerializer(typeof(Jaguar.Models.FlatFaultClassification));
                MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(item).Replace(" xsi:type=\"lineRequirement\"","")));
                Jaguar.Models.FlatFaultClassification? obj = serializer2.Deserialize(memStream) as Jaguar.Models.FlatFaultClassification;

                if (obj != null)
                {
                    AnalysisControlFlowNewModel analyse = converter.Convert(obj, parameters.ProjectKey);
                    return _client.AnalysisControlFlowAsync(analyse);
                }
            }

            return Task.CompletedTask;
        }
    }
}
