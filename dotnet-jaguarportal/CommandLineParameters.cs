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

        [Option("prNumber", Required = false, HelpText = "Set Pull Request Number.")]
        public string? PullRequestNumber { get; set; }

        [Option("prBranch", Required = false, HelpText = "Set Pull Request Branch.")]
        public string? PullRequestBranch { get; set; }

        [Option("prBase", Required = false, HelpText = "Set Pull Request Base from github.")]
        public string? PullRequestBase { get; set; }

        [Option("repo", Required = false, HelpText = "Set Repository.")]
        public string? Repository { get; set; }

        [Option("provider", Required = false, HelpText = "Set Pull Request Provider (default: github).", Default = "github")]
        public string? Provider { get; set; }

        [Option('j', "pathResult", Required = false, HelpText = "Set SBFL path result.")]
        public string? SBFLPathResult { get; set; }

        [Option('l', "pathLocal", Required = true, HelpText = "Set local path project.")]
        public string? LocalPath { get; set; }

        [Option('t', "pathTarget", Required = true, HelpText = "Set path target classes.")]
        public string? PathTarget { get; set; }

        [Option("botAccessToken", Required = false, HelpText = "Set access token to bot write comment.")]
        public string? BotAccessToken { get; set; }

        [Option("runId", Required = false, HelpText = "Set run id from github run CI/CD")]
        public string? RunId { get; set; }

        [Option("runAttempt", Required = false, HelpText = "Set run attempt from github run CI/CD")]
        public string? RunAttempt { get; set; }
    }
}
