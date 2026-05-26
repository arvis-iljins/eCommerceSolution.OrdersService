# OrdersService

Handles order creation, retrieval, updates, and deletion. Calls UsersService to resolve user details on orders.

## Stack

- .NET 10
- MongoDB
- FluentValidation
- AutoMapper

## Run

```bash
cd OrdersMicroservice.API
dotnet run
```

Swagger: **http://localhost:5080/swagger**

## Prerequisites

MongoDB running on `localhost:27017`. Start with:

```bash
brew services start mongodb-community
```

## Environment Variables

| Variable | Default | Description |
|---|---|---|
| `MONGO_HOST` | localhost | MongoDB host |
| `MONGO_PORT` | 27017 | MongoDB port |

## Endpoints

| Method | Route | Description |
|---|---|---|
| GET | `/api/orders` | Get all orders |
| GET | `/api/orders/search` | Search orders |
| GET | `/api/orders/{id}` | Get order by ID |
| POST | `/api/orders` | Create order |
| PUT | `/api/orders` | Update order |
| DELETE | `/api/orders/{id}` | Delete order |

## Project Structure

```
OrdersService/
├── OrdersMicroservice.API/  # Controllers, middleware, Program.cs
├── BusinessLogicLayer/      # DTOs, services, validators, HttpClients
│   └── HttpClients/         # UsersMicroserviceClient (calls UsersService)
└── DataAccessLayer/         # MongoDB repository
```
