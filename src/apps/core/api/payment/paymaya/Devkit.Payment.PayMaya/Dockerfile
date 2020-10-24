#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["apps/core/api/payment/paymaya/Devkit.Payment.PayMaya/Devkit.Payment.PayMaya.csproj", "apps/core/api/payment/paymaya/Devkit.Payment.PayMaya/"]
COPY ["apps/core/api/communication/Devkit.Communication.Payment/Devkit.Communication.Payment.csproj", "apps/core/api/communication/Devkit.Communication.Payment/"]
COPY ["apps/core/sdk/communication/Devkit.ServiceBus/Devkit.ServiceBus.csproj", "apps/core/sdk/communication/Devkit.ServiceBus/"]
RUN dotnet restore "apps/core/api/payment/paymaya/Devkit.Payment.PayMaya/Devkit.Payment.PayMaya.csproj"
COPY . .
WORKDIR "/src/apps/core/api/payment/paymaya/Devkit.Payment.PayMaya"
RUN dotnet build "Devkit.Payment.PayMaya.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Devkit.Payment.PayMaya.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Devkit.Payment.PayMaya.dll"]