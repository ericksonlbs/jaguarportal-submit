using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_jaguarportal.JaguarPortal.Models
{
    public class ClassAnalyze
    {
        public string? FullName { get; set; }
        public string? Path { get; set; }
        public string? Code { get; set; }
        public List<LineAnalyze>? Lines { get; set; }

    }
}
