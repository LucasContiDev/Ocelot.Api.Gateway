FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /sln

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /sln
COPY --from=build-env /sln/out .

ENTRYPOINT ["dotnet", "Ocelot.Search.APIGateway.dll"]