FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY [" E-Commerce-Api-with-.NET-8.0/E-Commerce-Api-with-.NET-8.0.csproj", " E-Commerce-Api-with-.NET-8.0/"]
RUN dotnet restore " E-Commerce-Api-with-.NET-8.0/E-Commerce-Api-with-.NET-8.0.csproj"
COPY . .
WORKDIR "/src/ E-Commerce-Api-with-.NET-8.0"
RUN dotnet build "./E-Commerce-Api-with-.NET-8.0.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./E-Commerce-Api-with-.NET-8.0.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-Commerce-Api-with-.NET-8.0.dll"]
