FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Crawler/Crawler.Core/ExchangeTypes.dll", "Crawler.Core/"]
COPY ["Crawler/Crawler.Core/Crawler.Core.csproj", "Crawler.Core/"]
COPY ["Crawler/Crawler.Database/Crawler.Database.csproj", "Crawler.Database/"]
COPY ["Crawler/Crawler.Main/Crawler.Main.csproj", "Crawler.Main/"]
RUN dotnet restore "Crawler.Main/Crawler.Main.csproj"
COPY Crawler/ .
RUN dotnet build "Crawler.Main/Crawler.Main.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Crawler.Main/Crawler.Main.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Crawler.Main.dll"]
