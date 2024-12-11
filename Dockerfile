# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

# SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Receipt-Processor.csproj", "./"]
RUN dotnet restore "./Receipt-Processor.csproj"
COPY . .
RUN dotnet build "Receipt-Processor.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Receipt-Processor.csproj" -c Release -o /app/publish

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Receipt-Processor.dll"]