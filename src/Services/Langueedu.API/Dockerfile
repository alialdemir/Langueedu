FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

#install debugger for NET Core
RUN apt-get update
RUN apt-get install -y unzip
RUN curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l ~/vsdbg

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY . .

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