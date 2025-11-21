# runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://0.0.0.0:5000

# build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY APIKarakatsiya/APIKarakatsiya.csproj ./
RUN dotnet restore APIKarakatsiya.csproj

COPY APIKarakatsiya/ ./
RUN dotnet publish APIKarakatsiya.csproj -c Release -o /app/publish /p:UseAppHost=false

# final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "APIKarakatsiya.dll"]