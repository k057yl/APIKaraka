FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://0.0.0.0:5000

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY APIKarakatsiya/APIKarakatsiya.csproj APIKarakatsiya/
RUN dotnet restore APIKarakatsiya/APIKarakatsiya.csproj

COPY . .
RUN dotnet publish APIKarakatsiya/APIKarakatsiya.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "APIKarakatsiya.dll"]