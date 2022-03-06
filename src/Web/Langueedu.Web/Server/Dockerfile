FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY . .

COPY ["src/Web/Langueedu.Web/Client/Langueedu.Web.Client.csproj", "Web/Langueedu.Web/Client/"]
COPY ["src/Web/Langueedu.Web/Langueedu.Web.Components/Langueedu.Web.Components.csproj", "Web/Langueedu.Web/Langueedu.Web.Components/"]
COPY ["src/Web/Langueedu.Web/Langueedu.Web.Pages/Langueedu.Web.Pages.csproj", "Web/Langueedu.Web/Langueedu.Web.Pages/"]
COPY ["src/Web/Langueedu.Web/Shared/Langueedu.Web.Shared.csproj", "Web/Langueedu.Web/Shared/"]
COPY ["src/Shared/Langueedu.Sdk/Langueedu.Sdk.csproj", "Shared/Langueedu.Sdk/"]
COPY ["src/Web/Langueedu.Web/Server/Langueedu.Web.Server.csproj", "Web/Langueedu.Web/Server/"]
RUN dotnet restore "./Server/Langueedu.Web.Server.csproj"
COPY . .

RUN dotnet build "./src/Web/Langueedu.Web/Server/Langueedu.Web.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./src/Web/Langueedu.Web/Server/Langueedu.Web.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Langueedu.Web.Server.dll"]