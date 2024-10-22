#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Use Microsoft ASP.NET image for runtime environment of the ASP.NET Core Application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

#Switcth to the 'app' user for security reasons, avoid running the container as root
USER app

#Set the working directory inside the container to /app
WORKDIR /app

#Expose ports 8080 and 8081 for communication
EXPOSE 8080
EXPOSE 8081

#Use Microsoft SDK image for the build stage. This image contain the SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

#Define an argument for the build configuration (default: Release)
ARG BUILD_CONFIGURATION=Release

#Set the working directory inside the container to /src for building the app
WORKDIR /src

#Copy the project files for the Web API and other class libraries
COPY ["BeanJournal_BackEnd/BeanJournal_BackEnd.csproj", "BeanJournal_BackEnd/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["Repositories/Repositories.csproj", "Repositories/"]
COPY ["RepositoryContracts/RepositoryContracts.csproj", "RepositoryContracts/"]
COPY ["ServiceContracts/ServiceContracts.csproj", "ServiceContracts/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["BeanJournal_BackEnd.Tests/BeanJournal_BackEnd.Tests.csproj", "BeanJournal_BackEnd.Tests/"]

#Restore dependencies and packages for the Web API project
RUN dotnet restore "./BeanJournal_BackEnd/BeanJournal_BackEnd.csproj"

#Copy the entire project code into the container, including the source files
COPY . .

#Change the working directory to the Web API project folder
WORKDIR "/src/BeanJournal_BackEnd"

#Build the Web API project in the specified build configuration and output the artifacts to /app/build
RUN dotnet build "./BeanJournal_BackEnd.csproj" -c $BUILD_CONFIGURATION -o /app/build

#Publishing the app. Uses the build stage
FROM build AS publish

#Define the build configuration argument again for consistency
ARG BUILD_CONFIGURATION=Release

#Publish the application to the /app/publish directory, diasbling the UseAppHost option, generate neccessary files to run te project
RUN dotnet publish "./BeanJournal_BackEnd.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

#Final stage, which will run the application
FROM base AS final

#Set the working directory for the final stage to /app
WORKDIR /app

#Copy the published application from the publish stage to the /app directory
COPY --from=publish /app/publish .

#Create a directory inside the container to store the HTTPS certificate
RUN mkdir -p /app/certificates

#Copy the HTTPS certificate into the container at /app/certificates
COPY  ./BeanJournal_BackEnd/certificate/BeanJournal_BackEnd.pfx /app/certificates

#Set the entry point for the container to run the application using the .NET runtime
ENTRYPOINT ["dotnet", "BeanJournal_BackEnd.dll"]