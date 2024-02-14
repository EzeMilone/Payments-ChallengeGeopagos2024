#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Stage 1: Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080  # Expose port for API
EXPOSE 8081  # Expose port for Swagger UI (optional)

# Stage 2: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Payments-ChallengeGeopagos2024.csproj", "."]
RUN dotnet restore "./././Payments-ChallengeGeopagos2024.csproj"
COPY . .
RUN dotnet build "./Payments-ChallengeGeopagos2024.csproj" -c Release -o /app/build

# Stage 3: Publish
FROM build AS publish
RUN dotnet publish "./Payments-ChallengeGeopagos2024.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=base /app/swagger/swagger-ui.html /app/swagger/index.html  # Copy Swagger UI HTML (optional)
ENTRYPOINT ["dotnet", "Payments-ChallengeGeopagos2024.dll"]
