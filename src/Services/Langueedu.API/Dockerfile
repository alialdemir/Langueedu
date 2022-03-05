FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY . .

COPY ["/src/Core/Langueedu.Infrastructure/Certificate/aspnetapp.pfx", "/https/aspnetapp.pfx"]

COPY ["src/Services/Langueedu.API/Langueedu.API.csproj", "./Langueedu.API/"]
RUN dotnet restore "./Langueedu.API/Langueedu.API.csproj"
COPY . .

RUN dotnet build "./src/Services/Langueedu.API/Langueedu.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./src/Services/Langueedu.API/Langueedu.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Langueedu.API.dll"]