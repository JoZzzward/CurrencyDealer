FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Converter/Converter.Core/ExchangeTypes.dll", "Converter.Core/"]
COPY ["Converter/Converter.Main/ExchangeTypes.dll", "Converter.Main/"]
COPY ["Converter/Converter.Core/Converter.Core.csproj", "Converter.Core/"]
COPY ["Converter/Converter.Main/Converter.Main.csproj", "Converter.Main/"]
RUN dotnet restore "Converter.Main/Converter.Main.csproj"
COPY Converter/ .
RUN dotnet build "Converter.Main/Converter.Main.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Converter.Main/Converter.Main.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Converter.Main.dll"]