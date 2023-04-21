using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_jaguarportal.Interfaces
{
   
    public interface IJaguarPortalConverter<T>
    {
        public AnalysisControlFlowNewModel Convert(T model, string projectID);
    }
}
