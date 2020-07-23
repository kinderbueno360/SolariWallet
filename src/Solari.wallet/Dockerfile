#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["solari.wallet/Solari.Wallet.csproj", "solari.wallet/"]
COPY ["Solari.Wallet.Applicaton/Solari.Wallet.Application.csproj", "Solari.Wallet.Applicaton/"]
COPY ["Solari.Wallet.Domain/Solari.Wallet.Domain.csproj", "Solari.Wallet.Domain/"]
COPY ["Solari.Wallet.Domain.Core/Solari.Wallet.Domain.Core.csproj", "Solari.Wallet.Domain.Core/"]
RUN dotnet restore "solari.wallet/Solari.Wallet.csproj"
COPY . .
WORKDIR "/src/solari.wallet"
RUN dotnet build "Solari.Wallet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Solari.Wallet.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Solari.Wallet.dll"]
