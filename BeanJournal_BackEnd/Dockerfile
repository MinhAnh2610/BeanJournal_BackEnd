#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BeanJournal_BackEnd/BeanJournal_BackEnd.csproj", "BeanJournal_BackEnd/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["Repositories/Repositories.csproj", "Repositories/"]
COPY ["RepositoryContracts/RepositoryContracts.csproj", "RepositoryContracts/"]
COPY ["ServiceContracts/ServiceContracts.csproj", "ServiceContracts/"]
COPY ["Services/Services.csproj", "Services/"]
RUN dotnet restore "./BeanJournal_BackEnd/BeanJournal_BackEnd.csproj"
COPY . .
WORKDIR "/src/BeanJournal_BackEnd"
RUN dotnet build "./BeanJournal_BackEnd.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BeanJournal_BackEnd.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN mkdir -p /app/certificates
COPY  ./BeanJournal_BackEnd/certificate/BeanJournal_BackEnd.pfx /app/certificates

ENTRYPOINT ["dotnet", "BeanJournal_BackEnd.dll"]