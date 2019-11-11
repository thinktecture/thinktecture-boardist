# Build Image
FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/out .
COPY --from=build-env /app/Assets ./Assets
ENTRYPOINT ["dotnet", "Thinktecture.Boardist.WebApi.dll"]
