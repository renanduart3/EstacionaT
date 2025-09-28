#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EstacionaT.API/EstacionaT.API.csproj", "EstacionaT.API/"]
COPY ["EstacionaT.Domain/EstacionaT.Domain.csproj", "EstacionaT.Domain/"]
COPY ["EstacionaT.Infrastructure/EstacionaT.Infrastructure.csproj", "EstacionaT.Infrastructure/"]
RUN dotnet restore "EstacionaT.API/EstacionaT.API.csproj"
COPY . .
WORKDIR "/src/EstacionaT.API"
RUN dotnet build "EstacionaT.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EstacionaT.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EstacionaT.API.dll"]