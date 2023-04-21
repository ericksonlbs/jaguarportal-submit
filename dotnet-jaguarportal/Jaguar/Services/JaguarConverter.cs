using dotnet_jaguarportal.Interfaces;
using dotnet_jaguarportal.JaguarPortal.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_jaguarportal.Jaguar.Services
{
    internal class JaguarConverter : IJaguarPortalConverter<Models.FlatFaultClassification>
    {
        private readonly CommandLineParameters _parameters;
        private readonly ILogger<JaguarConverter> _logger;

        public JaguarConverter(CommandLineParameters parameters, ILogger<JaguarConverter> logger)
        {
            _parameters = parameters;
            _logger = logger;
        }

        public AnalysisControlFlowNewModel Convert(Models.FlatFaultClassification model, string projectID)
        {
            var analysisControlFlow = new AnalysisControlFlowNewModel()
            {
                ProjectID = projectID,
                Heuristic = model.heuristic,
                Classes = new List<ClassAnalyze>(),
                FailedTests = 1,
                TotalTests = 1
            };

            foreach (var item in model.requirements.GroupBy(x => x.name))
            {
                ClassAnalyze myClass = new ClassAnalyze()
                {
                    FullName = item.Key,
                    Lines = new List<LineAnalyze>(),
                    Path = item.Key.Replace('.', Path.PathSeparator),
                    Code = getCode(item.Key)
                };

                foreach (var line in item)
                {
                    myClass.Lines.Add(new LineAnalyze()
                    {
                        Cef = line.cef,
                        Cep = line.cep,
                        Cnf = line.cnf,
                        Cnp = line.cnp,
                        NumberLine = line.location,
                        SuspiciousValue = double.Parse(line.suspiciousvalue.ToString()),
                        Method = getMethod(item.Key, line.number)
                    });
                }

                analysisControlFlow.Classes.Add(myClass);
            }

            return analysisControlFlow;
        }

        private string getMethod(string item, int line)
        {
            return "Not Implemented";
        }

        private string getCode(string item)
        {
            List<string> paths = new();
            string textFile = string.Empty;

            if (_parameters.PathTarget != null)
                paths.Add(_parameters.PathTarget);
            else
                _logger.LogWarning("Target Path: '{PathTarget}' not found.", _parameters.PathTarget);

            paths.AddRange(item.Split('.'));

            string file = $"{Path.Combine(paths.ToArray())}.java";

            if (File.Exists(file))
                textFile = File.ReadAllText(file);
            else
                _logger.LogWarning("File: '{file}' not found.", file);

            return textFile;
        }

    }
}
