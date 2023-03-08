#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /api

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["domain/domain.csproj", "domain/"]
COPY ["api/api.csproj", "api/"]
RUN dotnet restore "api/api.csproj"
COPY . .

WORKDIR "/src/api"
RUN dotnet build "api.csproj" -c Release -o /api/build

FROM build AS publish
RUN dotnet publish "api.csproj" -c Release -o /api/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /api
COPY --from=publish /api/publish .
ENTRYPOINT ["dotnet", "api.dll"]