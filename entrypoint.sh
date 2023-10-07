#!/bin/bash
set -o pipefail
set -eu

# Check required parameters has a value
if [ -z "$INPUT_JAGUARPORTALPROJECTKEY" ]; then
    echo "Input parameter jaguarPortalProjectKey is required"
    exit 1
fi
if [ -z "$INPUT_JAGUARPORTALCLIENTID" ]; then
    echo "Input parameter jaguarPortalClintId is required"
    exit 1
fi
if [ -z "$INPUT_JAGUARPORTALCLIENTSECRET" ]; then
    echo "Input parameter jaguarPortalClientSecret is required"
    exit 1
fi

# List Environment variables that's set by Github Action input parameters (defined by user)
echo "Github Action input parameters"
echo "INPUT_JAGUARPORTALPROJECTKEY: $INPUT_JAGUARPORTALPROJECTKEY"
echo "INPUT_JAGUARPORTALHOSTURL: $INPUT_JAGUARPORTALHOSTURL"

# Use APIKey with secretes on Github Action 
#       jaguarPortalClientId: ${{ secrets.JAGUARPORTAL_CLIENTID }}
#       jaguarPortalClientSecret: ${{ secrets.JAGUARPORTAL_CLIENTSECRET }}

# Environment variables automatically set by Github Actions automatically passed on to the docker container
#
# Example pull request
# GITHUB_REPOSITORY=theowner/therepo
# GITHUB_EVENT_NAME=pull_request
# GITHUB_REF=refs/pull/1/merge
# GITHUB_HEAD_REF=somenewcodewithouttests
# GITHUB_BASE_REF=master
#
# Example normal push
# GITHUB_REPOSITORY=theowner/therepo
# GITHUB_EVENT_NAME="push"
# GITHUB_REF=refs/heads/master
# GITHUB_HEAD_REF=""
# GITHUB_BASE_REF=""

# ---------------------------------------------
# DEBUG: How to run container manually
# ---------------------------------------------
# export JAGUARPORTAL_APIKEY="your_token_from_jaguarportal"

# Simulate Github Action input variables  
# export INPUT_JAGUARPORTALPROJECTKEY="your_projectkey"
# export INPUT_JAGUARPORTALHOSTURL="https://jaguarportalcloud.io"

# Simulate Github Action built-in environment variables
# export GITHUB_REPOSITORY=theowner/therepo
# export GITHUB_EVENT_NAME="push"
# export GITHUB_REF=refs/heads/master
# export GITHUB_HEAD_REF=""
# export GITHUB_BASE_REF=""
#
# Build local Docker image
# docker build -t jaguarportal-submit .
# Execute Docker container
# docker run --name jaguarportal-submit --workdir /github/workspace --rm -e INPUT_JAGUARPORTALPROJECTKEY -e INPUT_JAGUARPORTALPROJECTNAME -e INPUT_JAGUARPORTALHOSTURL -e JAGUARPORTAL_APIKEY -e GITHUB_EVENT_NAME -e GITHUB_REPOSITORY -e GITHUB_REF -e GITHUB_HEAD_REF -e GITHUB_BASE_REF -v "/var/run/docker.sock":"/var/run/docker.sock" -v $(pwd):"/github/workspace" jaguarportal-submit

#-----------------------------------
# Send to JaguarPortal
#-----------------------------------
jaguarportal_cmd="/root/.dotnet/tools/dotnet-jaguarportal -p \"${INPUT_JAGUARPORTALPROJECTKEY}\" -i \"${INPUT_JAGUARPORTALCLIENTID}\" -s \"${INPUT_JAGUARPORTALCLIENTSECRET}\" -h \"${INPUT_JAGUARPORTALHOSTURL}\" -j \"/github/workspace$INPUT_JAGUARPORTALANALYSISPATH\"  -t \"/github/workspace$INPUT_JAGUARPORTALSOURCEPATH\"  -l \"$INPUT_JAGUARPORTALSOURCEPATH\" --repo=\"$GITHUB_REPOSITORY\" --provider=github --runId=\"$GITHUB_RUN_ID\" --runAttempt=\"$GITHUB_RUN_ATTEMPT\" "

if [ ! -z "$INPUT_JAGUARPORTALGITHUBACCESSTOKEN" ]; then
    jaguarportal_cmd="$jaguarportal_cmd --botAccessToken=\"${INPUT_JAGUARPORTALGITHUBACCESSTOKEN}\" "
fi

# Check Github environment variable GITHUB_EVENT_NAME to determine if this is a pull request or not. 
if [[ $GITHUB_EVENT_NAME == 'pull_request' ]]; then
    # JaguarPortal wants these variables if build is started for a pull request
    # JaguarPortal parameters: https://github.com/ericksonlbs/JaguarPortal/wiki/API-Documentation/
    # prNumber	                Unique identifier of your PR. Must correspond to the key of the PR in GitHub or TFS. E.G.: 5
    # prBranch	            The name of your PR Ex: feature/my-new-feature
    # prBase	            The long-lived branch into which the PR will be merged. Default: master E.G.: master

    # Extract Pull Request numer from the GITHUB_REF variable
    PR_NUMBER=$(echo "$GITHUB_REF" | awk 'BEGIN { FS = "/" } ; { print $3 }')

    # Add pull request specific parameters
    jaguarportal_cmd="$jaguarportal_cmd --prNumber=\"$PR_NUMBER\" --prBranch=\"$GITHUB_HEAD_REF\" --prBase=\"$GITHUB_BASE_REF\" "

fi

#-----------------------------------
# Execute shell commands
#-----------------------------------
echo "Shell commands"
path="/github/workspace$INPUT_JAGUARPORTALANALYSISPATH"
if [ -d  "$path" ]; then
    echo "directory '$path' exists"

    #Run command
    echo "jaguarportal_cmd: $jaguarportal_cmd"
    sh -c "$jaguarportal_cmd"
else
    echo "directory '$path' not exists"
fi
