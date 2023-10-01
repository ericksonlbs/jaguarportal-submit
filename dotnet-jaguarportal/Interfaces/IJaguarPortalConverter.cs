using dotnet_jaguarportal.Jaguar2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_jaguarportal.Interfaces
{
   
    public interface IJaguarPortalConverter<T>
    {
        public Tuple<AnalysisControlFlowModel, IEnumerable<ClassAnalysisModel>> Convert(T? model,
            string? projectKey,
            string? repository,
            string? provider,
            string? pullRequestNumber,
            string? pullRequestBranch,
            string? pullRequestBase);
    }
}
