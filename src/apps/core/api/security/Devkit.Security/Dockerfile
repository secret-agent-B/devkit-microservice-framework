#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["services/security/Devkit.Security/Devkit.Security.csproj", "services/security/Devkit.Security/"]
COPY ["apps/logistics-app/common/domain/Devkit.Patterns/Devkit.Patterns.csproj", "apps/logistics-app/common/domain/Devkit.Patterns/"]
COPY ["apps/logistics-app/common/data/Devkit.Data/Devkit.Data.csproj", "apps/logistics-app/common/data/Devkit.Data/"]
COPY ["apps/logistics-app/common/webapi/Devkit.WebAPI/Devkit.WebAPI.csproj", "apps/logistics-app/common/webapi/Devkit.WebAPI/"]
COPY ["apps/logistics-app/common/infrastructure/Devkit.ServiceBus/Devkit.ServiceBus.csproj", "apps/logistics-app/common/infrastructure/Devkit.ServiceBus/"]
RUN dotnet restore "services/security/Devkit.Security/Devkit.Security.csproj"
COPY . .
WORKDIR "/src/services/security/Devkit.Security"
RUN dotnet build "Devkit.Security.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Devkit.Security.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Devkit.Security.dll"]