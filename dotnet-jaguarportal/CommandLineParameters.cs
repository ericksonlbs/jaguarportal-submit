using CommandLine;

namespace dotnet_jaguarportal
{
    public class CommandLineParameters
    {
        [Option('p', "projectKey", Required = true, HelpText = "Set Project Key from your project in JaguarPortal.")]
        public string? ProjectKey { get; set; }

        [Option('i', "clientId", Required = true, HelpText = "Set Client ID from your account in JaguarPortal.")]
        public string? ClientId { get; set; }
        
        [Option('s', "clientSecret", Required = true, HelpText = "Set Client Secret from your account in JaguarPortal.")]
        public string? ClientSecret { get; set; }

        [Option('h', "hostUrl", Required = true, HelpText = "Set Host URL from your published JaguarPortal.")]
        public string? HostUrl { get; set; }

        [Option("prKey", Required = false, HelpText = "Set Pull Request Key.")]
        public string? PullRequestKey { get; set; }

        [Option("prBranch", Required = false, HelpText = "Set Pull Request Branch.")]
        public string? PullRequestBranch { get; set; }

        [Option("prBase", Required = false, HelpText = "Set Pull Request Base from github.")]
        public string? PullRequestBase { get; set; }

        [Option("prRepo", Required = false, HelpText = "Set Pull Request Repository.")]
        public string? PullRequestRepository { get; set; }

        [Option("prProvider", Required = false, HelpText = "Set Pull Request Provider (default: github).", Default = "github")]
        public string? PullRequestProvider { get; set; }

        [Option('j', "pathResult", Required = false, HelpText = "Set SBFL path result.")]
        public string? SBFLPathResult { get; set; }

        [Option('l', "pathLocal", Required = true, HelpText = "Set local path project.")]
        public string? LocalPath { get; set; }

        [Option('t', "pathTarget", Required = true, HelpText = "Set path target classes.")]
        public string? PathTarget { get; set; }

        [Option('f', "formatSBFLFile", Required = false, HelpText = "Set format SBFL File (CSV or XML) (default: CSV).", Default = "CSV")]
        public string? FormatSBFLFile { get; set; }
    }
}
