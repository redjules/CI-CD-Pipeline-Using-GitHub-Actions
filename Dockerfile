FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY src/GitHubActionsDemo.Api/*.csproj ./GitHubActionsDemo.Api/
COPY src/GitHubActionsDemo.Api.Sdk/*.csproj ./GitHubActionsDemo.Api.Sdk/
COPY src/GitHubActionsDemo.Persistance/*.csproj ./GitHubActionsDemo.Persistance/
COPY src/GitHubActionsDemo.Service/*.csproj ./GitHubActionsDemo.Service/

WORKDIR /app/GitHubActionsDemo.Api
RUN dotnet restore

WORKDIR /app
COPY src/GitHubActionsDemo.Api/. ./GitHubActionsDemo.Api/
COPY src/GitHubActionsDemo.Api.Sdk/. ./GitHubActionsDemo.Api.Sdk/
COPY src/GitHubActionsDemo.Persistance/. ./GitHubActionsDemo.Persistance/
COPY src/GitHubActionsDemo.Service/. ./GitHubActionsDemo.Service/
WORKDIR /app/GitHubActionsDemo.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
EXPOSE 5275/tcp
ENV ASPNETCORE_URLS http://*:5275

COPY --from=build /app/GitHubActionsDemo.Api/out ./
ENTRYPOINT ["dotnet", "GitHubActionsDemo.Api.dll"]