using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_jaguarportal.JaguarPortal.Models
{
    public class AnalysisControlFlow
    {
        public string? ProjectID { get; set; }
        public List<ClassAnalyze>? Classes { get; set; }
        public string? Heuristic { get; set; }
        public int? TotalTests { get; set; }
        public int? FailedTests { get; set; }

    }
}
