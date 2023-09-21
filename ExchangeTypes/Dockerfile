FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ExchangeTypes/ExchangeTypes", "ExchangeTypes/"]
RUN dotnet restore "ExchangeTypes/ExchangeTypes.csproj"
COPY . .
WORKDIR "/src/ExchangeTypes"
RUN dotnet build "ExchangeTypes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExchangeTypes.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExchangeTypes.dll"]

