using dotnet_jaguarportal.Interfaces;
using dotnet_jaguarportal.JaguarPortal.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_jaguarportal.Jaguar2.Services
{
    internal class Jaguar2Converter : IJaguarPortalConverter<List<Models.Jaguar2Model>>
    {
        private readonly CommandLineParameters _parameters;
        private readonly ILogger<Jaguar2Converter> _logger;

        public Jaguar2Converter(CommandLineParameters parameters, ILogger<Jaguar2Converter> logger)
        {
            _parameters = parameters;
            _logger = logger;
        }

        public AnalysisControlFlowNewModel Convert(List<Models.Jaguar2Model> model, string projectID)
        {
            var analysisControlFlow = new AnalysisControlFlowNewModel()
            {
                ProjectID = projectID,
                Heuristic = "",
                Classes = new List<ClassAnalyze>(),
                FailedTests = 1,
                TotalTests = 1
            };

            foreach (var item in model)
            {
                bool existsClass = analysisControlFlow.Classes.Any(x => x.FullName == item.FullName);
                ClassAnalyze myClass;

                if (existsClass)
                    myClass = analysisControlFlow.Classes.First(x => x.FullName == item.FullName);
                else
                    myClass = new ClassAnalyze()
                    {
                        FullName = item.FullName,
                        Lines = new List<LineAnalyze>(),
                        Path = item.FullName.Replace('.', Path.PathSeparator),
                        Code = getCode(item.FullName)
                    };

                myClass.Lines.Add(new LineAnalyze()
                {
                    Cef = item.CEF,
                    Cep = item.CEP,
                    Cnf = item.CNF,
                    Cnp = item.CNP,
                    NumberLine = item.NumberLine,
                    SuspiciousValue = double.Parse(item.SuspiciousValue.ToString()),
                    Method = getMethod(item.FullName, item.NumberLine)
                });

                if (!existsClass)
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
