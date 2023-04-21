using dotnet_jaguarportal.Interfaces;
using dotnet_jaguarportal.Jaguar2.Models;
using dotnet_jaguarportal.JaguarPortal.Interfaces;
using dotnet_jaguarportal.JaguarPortal.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace dotnet_jaguarportal.JaguarPortal.Services
{
    internal class JaguarPortalService : IJaguarPortalService
    {
        private readonly swaggerClient _client;
        private readonly IJaguarPortalConverter<Jaguar.Models.FlatFaultClassification> converterXML;
        private readonly IJaguarPortalConverter<List<Jaguar2.Models.Jaguar2Model>> converterCSV;
        private readonly CommandLineParameters? parameters;

        public JaguarPortalService(IHttpClientFactory httpClientFactory, IJaguarPortalConverter<Jaguar.Models.FlatFaultClassification> converterXML, IJaguarPortalConverter<List<Jaguar2.Models.Jaguar2Model>> converterCSV, CommandLineParameters parameters)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Add($"X-API-Key", $"{parameters?.ApiKey}");
            _client = new swaggerClient(parameters?.HostUrl, httpClient);
            this.converterCSV = converterCSV ?? throw new ArgumentNullException(nameof(converterCSV));
            this.converterXML = converterXML ?? throw new ArgumentNullException(nameof(converterXML));
            this.parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

        public Task SendAnalisysControlFlow()
        {
            if (parameters != null &&
                parameters.SBFLPathResult != null &&
                parameters.ProjectKey != null)
            {
                return parameters.FormatSBFLFile switch
                {
                    "XML" => SendAnalisysControlFlowXML(),
                    _ => SendAnalisysControlFlowCSV(),
                };
            }

            return Task.CompletedTask;
        }

        private Task SendAnalisysControlFlowXML()
        {
            foreach (var item in Directory.GetFiles(parameters.SBFLPathResult, "*.xml", SearchOption.TopDirectoryOnly))
            {
                XmlSerializer serializer2 = new XmlSerializer(typeof(Jaguar.Models.FlatFaultClassification));
                MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(item).Replace(" xsi:type=\"lineRequirement\"", "")));
                Jaguar.Models.FlatFaultClassification? obj = serializer2.Deserialize(memStream) as Jaguar.Models.FlatFaultClassification;

                if (obj != null)
                {
                    AnalysisControlFlowNewModel analyse = converterXML.Convert(obj, parameters.ProjectKey);
                    return _client.AnalysisControlFlowAsync(analyse);
                }
            }
            return Task.CompletedTask;
        }

        private Task SendAnalisysControlFlowCSV()
        {
            foreach (var item in Directory.GetFiles(parameters.SBFLPathResult, "*.csv", SearchOption.TopDirectoryOnly))
            {
                List<Jaguar2Model> obj = new();

                foreach (var line in File.ReadAllLines(item))
                {
                    Jaguar2Model lineObj = new Jaguar2Model(line);

                    //string notice = $"::notice file={lineObj.FileName},line={lineObj.NumberLine},endLine={lineObj.NumberLine},title=Suspecious-{lineObj.SuspiciousValue}::{JsonSerializer.Serialize(lineObj)}";
                    string notice = $"::warning file=./{parameters.PathTarget}/{lineObj.FullName}.java,line={lineObj.NumberLine},endLine={lineObj.NumberLine},title=Suspecious::Suspecious Text Message2";
                    Console.WriteLine(notice);

                    obj.Add(lineObj);
                }

                if (obj != null)
                {
                    AnalysisControlFlowNewModel analyse = converterCSV.Convert(obj, parameters.ProjectKey);
                    return _client.AnalysisControlFlowAsync(analyse);
                }

            }
            return Task.CompletedTask;
        }
    }
}
