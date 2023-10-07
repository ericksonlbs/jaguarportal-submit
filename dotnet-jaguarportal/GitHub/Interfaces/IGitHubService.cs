using Octokit;

namespace dotnet_jaguarportal.GitHub.Services
{
    public interface IGitHubService
    {
        /// <summary>
        /// Create comment in pull request
        /// </summary>
        /// <param name="owner">Owner repository</param>
        /// <param name="repo">Name repository</param>
        /// <param name="prNumber">PullRequest Number</param>
        /// <param name="comment">Comment</param>
        public Task<IssueComment?> CreateCommentPullRequest(string owner, string repo, int prNumber, string comment);
    }
}
