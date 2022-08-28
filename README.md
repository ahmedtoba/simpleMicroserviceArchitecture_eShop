# simpleMicroserviceArchitecture_eShop

**This is a simple e-shop app using Microservices Architecture.**
**The app is containerized using Docker and orchestrated using Kubernetes**


![microservice_diagram](https://user-images.githubusercontent.com/20687611/187051742-2dfb0ee6-f38a-43ff-8abd-91ce8817bd9e.png)

![kubernetes_Architecture](https://user-images.githubusercontent.com/20687611/187067321-ffcec593-b122-4071-a3c5-cf50b74015a8.png)

**Services Created**
- The App consists of 2 services (It's typically should be more than this but no time for this)
    - Catalog service 
    - Shopping Cart Service

## Documentation:
**Architectual and Desing Patterns used:**
- Catalog Service: N-Tier Architecture
- Cart Service: N-Tier Architecture
** The best Architectural pattern should've be used is Clean Arcitecture but not implemented here
- Repository Pattern to allow us to easily test our application with unit tests
- Event-driven Architecture and Async communication between services using RabbitMQ Message Bus
- Dependency Injection is used to ensure separation of concerns and loose coupling between all services and classes
- API Gateway Routing Pattern using Ingress Nginx

**The app is composed of 3 main services:**

### Catalog Service

**This Servcie is for CRUD Operation of the Product. It uses SQL Server as a Database**
**User Can add product to cart**
**Internal Diagram of Catalog Microservice**
![image](https://user-images.githubusercontent.com/20687611/187052091-085be62d-3710-447f-9d13-6826df0f280e.png)

### Cart Service

**The Cart service is responsible for operations on the user basket such as creating a user basket, adding product to Shopping Cart**
**It Uses Redis Cache for updating and retreiving user cart data**

### Tools used
- **Docker : For containerizing services**
- **Kubernetes : For Orchestrating containers and deployment**
- **Ingress Nginx API Gateway: Acts as the gateway to all services**
- **RabbitMQ Event Bus: Allows Async communication between services**

### Unit Testing
- **Xunit framework and Moq package are used for Unit Tests**
