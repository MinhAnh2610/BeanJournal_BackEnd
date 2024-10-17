
# BeanJournal_BackEnd

Welcome to the **BeanJournal** backend repository. This project is an API built using ASP.NET Core, designed to handle diary entries with features like file uploads, tagging, and image processing. The backend utilizes Docker and SQL Server for the database, with plans for PostgreSQL support.


## Features

- **JWT Authentication**: Secure user authentication and authorization.
- **Diary Entries**: Create, update, delete, and retrieve diary entries.
- **File Upload**: Upload and manage media attachments (images, PDFs).
- **Tagging**: Assign and manage tags for diary entries.
- **Image Processing**: Support for image enhancements (cropping, resizing) using Cloudinary.
- **Caching**: Redis-based caching for frequently accessed data like tags.
- **Database**: Currently using SQL Server, with plans for PostgreSQL support.
- **Dockerized Setup**: Easily run the application using Docker.
- **Swagger Documentation**: API documentation available via Swagger UI.


## Technologies

- **.NET 8**: Core framework for building the backend.
- **SQL Server**: Main database (PostgreSQL planned).
- **Cloudinary**: Image storage and processing.
- **Docker**: Containerization of the application.
- **Redis**: Caching layer for improved performance.
- **Serilog**: Log framework for loggging and analyzing data.
- **JWT**: JSON Web Token for secure authentication.


## Getting Started

### Prerequisites

- .NET SDK 8.0 or later
- Docker
- SQL Server or PostgreSQL (for local development)
- Redis (optional, for caching)

### Setup

1 Clone the repository:
   ```
   git clone https://github.com/your-username/BeanJournal_BackEnd.git
   cd BeanJournal_BackEnd
   ```

2 Install the required packages:

    dotnet restore

3 Create a ```appsettings.json``` file in the ```BeanJournal_BackEnd``` project directory and configure your database connection strings, Cloudinary credentials, JWT settings, etc.:

    {
        "AllowedHosts": "*",
        "CloudinarySettings": {
            "CloudName": "your_cloud_name",
            "ApiKey": "your_api_key",
            "ApiSecret": "your_api_secret"
        },
        "ConnectionStrings": {
            "DefaultConnection": "your_database_connection_string",
            "Redis": "localhost:6379"
        },
        "EmailConfiguration": {
            "From": "your_email",
            "SmtpServer": "smtp.gmail.com",
            "Port": your_port_number,
            "Username": "your_username",
            "Password": "your_password"
        },
        "Jwt": {
            "Issuer": "http://localhost:5246",
            "Audience": "http://localhost:5246",
            "Key": "your_super_secret_key",
            "EXPIRATION_MINUTES": 10
        },
        "Logging": {
            "LogLevel": {
                "Default": "Information",
                "Microsoft.AspNetCore": "Warning",
                "System": "Information"
            }
        },
        "RefreshToken": {
            "EXPIRATION_MINUTES": 60
        },
        "Serilog": {
            "MinimumLevel": "Debug",
            "Using": [
                "Serilog.Sinks.Console",
                "Serilog.Sinks.File",
                "Serilog.Sinks.MSSqlServer"
            ],
            "WriteTo": [
                {
                    "Name": "Console"
                },
                {
                    "Name": "File",
                    "Args": {
                        "path": "Logs/log.txt",
                        "rollingInterval": "Hour",
                        "fileSizeLimitBytes": 1048576,
                        "rollOnFileSizeLimit": true
                    }
                },
                {
                    "Name": "MSSqlServer",
                    "Args": {
                        "connectionString": "your_log_database_connection_string",
                        "tableName": "Logs",
                        "autoCreateSqlTable": true
                    }
                }
            ]
        }
    }

4 Run the application:

    dotnet run

5 To run using Docker, build and run the Docker container:

    docker-compose up --build


## API Documentation

The API is documented using Swagger. Once the application is running, you can access the Swagger UI at:

https://localhost:8081/swagger
