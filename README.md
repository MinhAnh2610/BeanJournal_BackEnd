
# BeanJournal

Welcome to the **BeanJournal** backend repository. This project is an API built using ASP.NET Core, designed to handle diary entries with features like file uploads, tagging, and image processing. The backend is deployed to Azure and utilizes Docker and SQL Server for the database, with plans for PostgreSQL support.


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


## Tech Stack

- **.NET 6**: Core framework for building the backend.
- **Azure Web App**: Hosting for the backend.
- **SQL Server (Azure)**: Main database (PostgreSQL planned).
- **Cloudinary**: Image storage and processing.
- **Docker**: Containerization of the application.
- **Redis**: Caching layer for improved performance.
- **JWT**: JSON Web Token for secure authentication.


## Getting Started

### Prerequisites

- .NET SDK 6.0 or later
- Docker
- SQL Server or PostgreSQL (for local development)
- Azure account (if deploying to Azure)
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
        "ConnectionStrings": {
            "DefaultConnection": "Your SQL Server or PostgreSQL connection string"
        },
        "Cloudinary": {
            "CloudName": "Your Cloudinary Cloud Name",
            "ApiKey": "Your Cloudinary API Key",
            "ApiSecret": "Your Cloudinary API Secret"
        },
        "JwtSettings": {
            "Secret": "Your JWT Secret Key",
            "Issuer": "BeanJournal",
            "Audience": "BeanJournalAudience"
        },
        "Redis": {
            "ConnectionString": "localhost:6379"
        }
    }
4 Run the application:

    dotnet run

5 To run using Docker, build and run the Docker container:

    docker-compose up --build


## API Documentation

The API is documented using Swagger. Once the application is running, you can access the Swagger UI at:

https://localhost:5001/swagger
