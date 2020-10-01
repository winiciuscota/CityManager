ARG VERSION=3.1-alpine

FROM mcr.microsoft.com/dotnet/core/sdk:$VERSION AS build
WORKDIR /app

# Copy csproj files first to optimize build process

COPY *.sln ./
COPY src/CityManager.Api/*.csproj ./src/CityManager.Api/
COPY src/CityManager.Data/*.csproj ./src/CityManager.Data/
COPY src/CityManager.Domain/*.csproj ./src/CityManager.Domain/

# RUN dotnet restore

# Copy everything else
COPY . ./
RUN dotnet publish src/CityManager.Api -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:$VERSION AS runtime
WORKDIR /app
COPY src/CityManager.Api/cities-seed.csv .
COPY --from=build /app/out/ .

# Update end machine and setup dependencies
RUN apk -U upgrade

ENTRYPOINT [ "dotnet", "CityManager.Api.dll" ]