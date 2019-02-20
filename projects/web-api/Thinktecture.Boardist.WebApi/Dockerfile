# Build Image
FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
COPY --from=build-env /app/Assets ./Assets
ENTRYPOINT ["dotnet", "Thinktecture.Boardist.WebApi.dll"]
