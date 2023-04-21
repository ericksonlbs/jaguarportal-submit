using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_jaguarportal.Jaguar2.Models
{
    /// <summary>
    /// Define csv model. Sample csv line: org/apache/commons/codec/binary/StringUtils,72,2,0,9,815,0.426401,/tmp/projectX/org/apache/commons/codec/binary/StringUtils.java
    /// </summary>
    internal class Jaguar2Model
    {
        /// <summary>
        /// Set
        /// </summary>
        /// <param name="line"></param>
        public Jaguar2Model(string line)
        {
            FullName = string.Empty;
            FileName = string.Empty;
            SetLineCSV(line);
        }

        /// <summary>
        /// Class name
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Line number
        /// </summary>
        public int NumberLine { get; set; }
        /// <summary>
        /// Executed and failed
        /// </summary>
        public int CEF { get; set; }
        /// <summary>
        /// Not executed and failed
        /// </summary>
        public int CNF { get; set; }
        /// <summary>
        /// Executed and Pass
        /// </summary>
        public int CEP { get; set; }
        /// <summary>
        /// Not executed and passed
        /// </summary>
        public int CNP { get; set; }
        /// <summary>
        /// Score classification SBFL
        /// </summary>
        public double SuspiciousValue { get; set; }
        /// <summary>
        /// File name
        /// </summary>
        public string FileName { get; set; }

        private const char csvSeparator = ',';

        private void SetLineCSV(string lineCSV)
        {
            string[] splited = lineCSV.Split(csvSeparator);

            if (splited.Length > 0)
                FullName = splited[0];
            if (splited.Length > 1 && int.TryParse(splited[1], out int n))
                NumberLine = n;
            if (splited.Length > 2 && int.TryParse(splited[2], out n))
                CEF = n;
            if (splited.Length > 3 && int.TryParse(splited[3], out n))
                CNF = n;
            if (splited.Length > 4 && int.TryParse(splited[4], out n))
                CEP = n;
            if (splited.Length > 5 && int.TryParse(splited[5], out n))
                CNP = n;
            if (splited.Length > 6 && double.TryParse(splited[6], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out double d))
                SuspiciousValue = d;
            if (splited.Length > 7)
                FileName = splited[7];
        }
    }
}
