using dotnet_jaguarportal.Interfaces;
using dotnet_jaguarportal.Jaguar2.Models;
using dotnet_jaguarportal.JaguarPortal.Models;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata.Ecma335;
using static System.Net.Mime.MediaTypeNames;

namespace dotnet_jaguarportal.Jaguar2.Services
{
    public class Jaguar2Converter : IJaguarPortalConverter<Jaguar2Model>
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
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(projectKey))
            {
                throw new ArgumentException($"'{nameof(projectKey)}' cannot be null or empty.", nameof(projectKey));
            }

            AnalysisControlFlowModel analysisControlFlow = new AnalysisControlFlowModel()
            {
                ProjectKey = projectKey,
                TestsFail = model.tests.fail,
                TestsPass = model.tests.pass
            };
            List<ClassAnalysisModel> classes = new List<ClassAnalysisModel>();

            if (model?.package == null || model?.package?.Count() == 0)
                return new Tuple<AnalysisControlFlowModel, IEnumerable<ClassAnalysisModel>>(analysisControlFlow, classes);

            foreach (var pkg in model.package)
            {
                foreach (var item in pkg.sourcefile)
                {
                    string fullname = Path.Combine(pkg.name, item.name);
                    if (Path.DirectorySeparatorChar == '\\')
                    {
                        fullname = fullname.Replace('/', '\\');
                    }
                    else if (Path.DirectorySeparatorChar == '\\')
                    {
                        fullname = fullname.Replace('\\', '/');
                    }

                    ClassAnalysisModel myClass = new ClassAnalysisModel()
                    {
                        FullName = fullname.Replace('\\', '/'),
                        Lines = new List<LineAnalysisModel>(),
                        Code = getCode(fullname)
                    };

                    if (item.line != null && item.line.Length > 0)
                    {
                        foreach (var line in item.line)
                        {
                            myClass.Lines.Add(new LineAnalysisModel()
                            {
                                Cef = line.cef,
                                Cep = line.cep,
                                Cnf = model.tests.fail - line.cef,
                                Cnp = model.tests.pass - line.cep,
                                NumberLine = line.nr,
                                SuspiciousValue = double.Parse(line.susp.ToString()),
                                Method = getMethod(item.name, line.nr)
                            });
                        }

                        classes.Add(myClass);
                    }
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
            byte[]? textFile = null;

            if (_parameters.PathTarget == null)
                _logger.LogWarning("Target Path: '{PathTarget}' not found.", _parameters.PathTarget);
            else
            {
                string file = $"{Path.Combine(_parameters?.PathTarget, item)}";

                if (File.Exists(file))
                    textFile = File.ReadAllBytes(file);
                else
                    _logger.LogWarning("File: '{file}' not found.", file);
            }

            return textFile;
        }
    }
}
