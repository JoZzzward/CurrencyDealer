FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ExchangeTypes/*.csproj", "ExchangeTypes/"]
RUN dotnet restore "ExchangeTypes/ExchangeTypes/ExchangeTypes.csproj"
COPY . .
WORKDIR "/src/ExchangeTypes"
RUN dotnet build "ExchangeTypes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExchangeTypes.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExchangeTypes.dll"]

#Привет ребят. Надеюсь на вашу помощь, я уже собаку сьел на этом, не могу проект запустить в docker-compose. Если кратко, есть 4 решения, в 3 из них проекты, а в 1 просто библиотека с DTO, которая ко всем подключается. При запуске выдает ошибку со скрина, пытался через COPY в dockerfile добавить библиотеку, не помогает.
