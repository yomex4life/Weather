from mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out


# Build runtime image
from mcr.microsoft.com/dotnet/aspnet:7.0 
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "Precipitation.dll"]
