using dotnet_jaguarportal.Jaguar.Interfaces;
using dotnet_jaguarportal.JaguarPortal.Models;
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
        public AnalysisControlFlowNewModel Convert(Models.FlatFaultClassification model, string projectID)
        {
            var analysisControlFlow = new AnalysisControlFlowNewModel()
            {
                ProjectID = projectID,
                Heuristic = model.heuristic,
                Classes = new List<ClassAnalyze>()
            };

            foreach (var item in model.requirements.GroupBy(x => x.name))
            {
                ClassAnalyze myClass = new ClassAnalyze()
                {
                    FullName = item.Key,
                    Lines = new List<LineAnalyze>(),
                    Path = item.Key.Replace(".", "/"),
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
                        NumberLine = line.number,
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
            return "NOTIMPLEMENTED";
        }

        private string getCode(string item)
        {
            return string.Empty;
        }

    }
}
