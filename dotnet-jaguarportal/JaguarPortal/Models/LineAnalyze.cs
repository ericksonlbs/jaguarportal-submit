using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_jaguarportal.JaguarPortal.Models
{
    public class LineAnalyze
    {
        public string? Method { get; set; }
        public int? NumberLine { get; set; }
        public int? CEF { get; set; }
        public int? CEP { get; set; }
        public int? CNF { get; set; }
        public int? CNP { get; set; }
        public double? SuspiciousValue { get; set; }
    }
}
