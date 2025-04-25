# E-Commerce API with .NET 8.0

## Project Overview
This project is a modular **E-Commerce API** built using **.NET 8.0**. It is designed to provide a scalable and robust backend system for managing e-commerce operations such as product management, order processing, authentication, and other related services. The project adopts a **microservices architecture** and leverages .NET 8.0's latest features to ensure high performance and scalability.

## Project Structure
The solution consists of the following components:

- **ApiGateway**: Acts as the facade for external clients, routing all requests to the respective microservices.
- **AuthorizationService**: handles Custom Registration Process
- **ProductService**: Manages the catalog of products, supporting CRUD operations.
- **OrderService**: Handles customer orders, including order creation, updates, and status tracking.

## Features
- **Microservices Architecture**: Each service has independent functionality that ensures modularity, scalability, and easy maintenance.
- **API Gateway**: Centralized point for routing and aggregating requests.
- **JWT Authentication**: Securely authenticate users using JWT tokens.
- **Docker Support**: The entire solution is containerized using **Docker** for consistent deployments.
- **Modern Frameworks**: Built with .NET 8.0 for improved performance and features.

## Technologies and Tools
The project uses the following tools and technologies:
The project uses the following technologies:
- **.NET 8.0**: Backend application framework.
- **ASP.NET Core 8**: Framework to build RESTful APIs.
- **Docker**: Used for containerization.
- **Keycloak**: Centralized authentication and identity management.
- **PostgreSQL**: Open-source relational database system.
- **FluentValidation**: For input validation.
- **MediatR**: Implements **Mediator Pattern** to support **CQRS (Command Query Responsibility Segregation)**, ensuring separation of write (commands) and read (queries) operations for better scalability and maintainability.
- **RabbitMQ**: Message broker for inter-service communication.
- **Ocelot**: API Gateway for routing microservices.


## Getting Started
Follow the steps below to get the application running locally.

### Prerequisites
- [Docker](https://www.docker.com/) installed

### Build and Run
1. Clone the repository:
   ```bash
   git clone <repository_url>
   cd E-Commerce-Api-with-.NET-8.0
   ```

2. Build the Docker containers:
   ```bash
   docker-compose up --build
   ```

3. Docker Compose will spin up the following services:
    - **ApiGateway** (Default: `http://localhost:5000`)
    - **AuthorizationService** (Default: `http://localhost:50001`)
    - **ProductService** (Default: `http://localhost:5003`)
    - **OrderService** (Default: `http://localhost:5002`)
    - **Keycloak** (Default: `http://localhost:8080`)
    - **RabbitMQ** (Default: `http://localhost:15672` for admin interface)


## Configuration
Key configuration files:
- **`appsettings.Docker.json`**: Contains environment-specific settings for services, e.g., database connection strings, message broker credentials.
- **`compose.yaml`**: Defines the multi-container Docker application setup.
- **Keycloak Settings**:
    - Realm: `E-Commerce` (default), can also be changed.
    - Admin User: `admin`,
    - Admin Password: `admin123`,
  - **Client**:
      - Create a client named `"my-api-client"`.
      - Configure the client as **public**.
     

> ⚠ You need to configure Keycloak manually for now. For more details, refer to [Keycloak Documentation](https://www.keycloak.org/documentation).

  

## API Documentation
- Swagger endpoints:
    - Individual services: `http://<host>:<service-port>/swagger`
  
## License
This project is licensed under the **MIT License**.

---


