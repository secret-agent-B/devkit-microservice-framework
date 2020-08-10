#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["services/gateway/Devkit.Gateway/Devkit.Gateway.csproj", "services/gateway/Devkit.Gateway/"]
COPY ["apps/logistics-app/common/webapi/Devkit.WebAPI/Devkit.WebAPI.csproj", "apps/logistics-app/common/webapi/Devkit.WebAPI/"]
COPY ["apps/logistics-app/common/infrastructure/Devkit.ServiceBus/Devkit.ServiceBus.csproj", "apps/logistics-app/common/infrastructure/Devkit.ServiceBus/"]
COPY ["apps/logistics-app/common/domain/Devkit.Patterns/Devkit.Patterns.csproj", "apps/logistics-app/common/domain/Devkit.Patterns/"]
COPY ["apps/logistics-app/common/data/Devkit.Data/Devkit.Data.csproj", "apps/logistics-app/common/data/Devkit.Data/"]
RUN dotnet restore "services/gateway/Devkit.Gateway/Devkit.Gateway.csproj"
COPY . .
WORKDIR "/src/services/gateway/Devkit.Gateway"
RUN dotnet build "Devkit.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Devkit.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Devkit.Gateway.dll"]