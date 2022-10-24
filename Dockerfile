FROM mcr.microsoft.com/dotnet/sdk:6.0

LABEL "com.github.actions.name"="jaguarportal-submit"
LABEL "com.github.actions.description"="Submit result analysis SBFL to Jaguar Portal."
LABEL "com.github.actions.icon"="check-square"
LABEL "com.github.actions.color"="blue"

LABEL "repository"="https://github.com/ericksonlbs/jaguarportal-submit"
LABEL "homepage"="https://github.com/ericksonlbs"
LABEL "maintainer"="Erickson Lima"

# Version numbers of used software
ENV DOTNETCORE_RUNTIME_VERSION=6.0 \
    JAGUARPORTAL_SUBMIT_DOTNET_TOOL_VERSION=1.0

# Add Microsoft Debian apt-get feed 
RUN wget https://packages.microsoft.com/config/debian/10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb

# Install the .NET Runtime for JaguarPortalSubmit
# The warning message "delaying package configuration, since apt-utils is not installed" is probably not an actual error, just a warning.
# We don't need apt-utils, we won't install it. The image seems to work even with the warning.
RUN apt-get update -y \
    && apt-get install --no-install-recommends -y apt-transport-https \
    && apt-get update -y \
    && apt-get install --no-install-recommends -y aspnetcore-runtime-$DOTNETCORE_RUNTIME_VERSION

# Install .NET global tool
RUN dotnet tool install --global dotnet-jaguarportal --version $JAGUARPORTAL_SUBMIT_DOTNET_TOOL_VERSION

# Cleanup
RUN apt-get -q -y autoremove \
    && apt-get -q clean -y \
    && rm -rf /var/lib/apt/lists/* /var/cache/apt/archives/*

ADD entrypoint.sh /entrypoint.sh

ENTRYPOINT ["/entrypoint.sh"]