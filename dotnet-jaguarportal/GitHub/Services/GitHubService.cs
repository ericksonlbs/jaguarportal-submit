using Octokit;

namespace dotnet_jaguarportal.GitHub.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly GitHubClient client;

        public GitHubService(GitHubClient client)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            this.client = client;
        }

        /// <summary>
        /// Create comment in pull request
        /// </summary>
        /// <param name="owner">Owner repository</param>
        /// <param name="repo">Name repository</param>
        /// <param name="prNumber">PullRequest Number</param>
        /// <param name="comment">Comment</param>
        public async Task<IssueComment?> CreateCommentPullRequest(string owner, string repo, int prNumber, string comment)
        {
            if (client.Credentials != null)
            {
                IssueComment? issueComment = await client.Issue.Comment.Create(owner, repo, prNumber, comment);
                return issueComment;
            }
            else
                return null;
        }
    }
}
