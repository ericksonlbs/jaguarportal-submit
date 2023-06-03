using dotnet_jaguarportal.Interfaces;
using dotnet_jaguarportal.Jaguar2.Models;
using dotnet_jaguarportal.JaguarPortal.Models;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata.Ecma335;

namespace dotnet_jaguarportal.Jaguar2.Services
{
    internal class Jaguar2Converter : IJaguarPortalConverter<Jaguar2Model>
    {
        private readonly CommandLineParameters _parameters;
        private readonly ILogger<Jaguar2Converter> _logger;

        public Jaguar2Converter(CommandLineParameters parameters, ILogger<Jaguar2Converter> logger)
        {
            _parameters = parameters;
            _logger = logger;
        }

        public Tuple<AnalysisControlFlowModel, IEnumerable<ClassAnalysisModel>> Convert(Jaguar2Model model, string projectKey)
        {
            AnalysisControlFlowModel analysisControlFlow = new AnalysisControlFlowModel()
            {
                ProjectKey = projectKey
            };
            List<ClassAnalysisModel> classes = new List<ClassAnalysisModel>();

            if (model?.Classes == null)
                return new Tuple<AnalysisControlFlowModel, IEnumerable<ClassAnalysisModel>>(analysisControlFlow, classes);

            foreach (var item in model.Classes)
            {
                ClassAnalysisModel myClass = new ClassAnalysisModel()
                {
                    FullName = item.name,
                    Lines = new List<LineAnalysisModel>(),
                    Code = getCode(item.name)
                };

                if (item.Lines != null && item.Lines.Length > 0)
                {
                    foreach (var line in item.Lines)
                    {
                        myClass.Lines.Add(new LineAnalysisModel()
                        {
                            Cef = line.cef,
                            Cep = line.cep,
                            Cnf = line.cnf,
                            Cnp = line.cnp,
                            NumberLine = line.nr,
                            SuspiciousValue = double.Parse(line.SuspiciousnessValue.ToString()),
                            Method = getMethod(item.name, line.nr)
                        });
                    }

                    classes.Add(myClass);
                }
            }

            return new Tuple<AnalysisControlFlowModel, IEnumerable<ClassAnalysisModel>>(analysisControlFlow, classes);
        }

        private string getMethod(string item, int line)
        {
            return "Not Implemented";
        }

        private byte[]? getCode(string item)
        {
            List<string> paths = new();
            byte[]? textFile = null;

            if (_parameters.PathTarget != null)
                paths.Add(_parameters.PathTarget);
            else
                _logger.LogWarning("Target Path: '{PathTarget}' not found.", _parameters.PathTarget);

            paths.AddRange(item.Split('.'));

            string file = $"{Path.Combine(paths.ToArray())}.java";

            if (File.Exists(file))
                textFile = File.ReadAllBytes(file);
            else
                _logger.LogWarning("File: '{file}' not found.", file);

            return textFile;
        }
    }
}
