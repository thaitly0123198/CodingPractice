FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY PracticeProblems.Api/PracticeProblems.Api.csproj PracticeProblems.Api/
COPY PracticeProblems.Core/PracticeProblems.Core.csproj PracticeProblems.Core/
COPY PracticeProblems.Data/PracticeProblems.Data.csproj PracticeProblems.Data/
COPY PracticeProblems.Services/PracticeProblems.Services.csproj PracticeProblems.Services/

RUN dotnet restore PracticeProblems.Api/PracticeProblems.Api.csproj

COPY . .

RUN dotnet publish PracticeProblems.Api/PracticeProblems.Api.csproj \
    -c Release \
    -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

RUN apt-get update \
    && apt-get install -y --no-install-recommends python3 \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

ENTRYPOINT ["dotnet", "PracticeProblems.Api.dll"]
