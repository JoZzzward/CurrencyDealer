FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Storage/Storage.Core/ExchangeTypes.dll", "Storage.Core/"]
COPY ["Storage/Storage.Main/ExchangeTypes.dll", "Storage.Main/"]
COPY ["Storage/Storage.Core/Storage.Core.csproj", "Storage.Core/"]
COPY ["Storage/Storage.Main/Storage.Main.csproj", "Storage.Main/"]
COPY ["Storage/Storage.Database/Storage.Database.csproj", "Storage.Database/"]
RUN dotnet restore "Storage.Main/Storage.Main.csproj"
COPY Storage/ .
RUN dotnet build "Storage.Main/Storage.Main.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Storage.Main/Storage.Main.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Storage.Main.dll"]

#Привет ребят. Надеюсь на вашу помощь, я уже собаку сьел на этом, не могу проект запустить в docker-compose. Если кратко, есть 2 решения(API, Library),API ссылается на библиотеку с DTO, библиотека находится в другом решении. При запуске выдает ошибку со скрина, пытался через COPY в dockerfile добавить библиотеку, не помогает.
