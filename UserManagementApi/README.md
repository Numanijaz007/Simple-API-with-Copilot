# User Management API

A simple ASP.NET Core Web API for managing users, built as part of the Back-End Development with .NET course project.

## Features

- **CRUD endpoints** for managing users (`GET`, `POST`, `PUT`, `DELETE`)
- **Validation** on user data (name, email format, age range) using data annotations
- **Custom middleware**:
  - `RequestLoggingMiddleware` — logs every incoming request, its response status code, and how long it took
  - `ApiKeyAuthMiddleware` — requires an `X-Api-Key` header on all requests (except Swagger UI)

## Project structure

```
UserManagementApi/
├── Controllers/
│   └── UsersController.cs
├── Middleware/
│   ├── RequestLoggingMiddleware.cs
│   └── ApiKeyAuthMiddleware.cs
├── Models/
│   └── User.cs
├── Services/
│   └── UserService.cs
├── Program.cs
└── UserManagementApi.csproj
```

## Running the project

```bash
dotnet restore
dotnet run
```

Then open `https://localhost:<port>/swagger` to explore the API.

## Authentication

All endpoints (except Swagger) require the header:

```
X-Api-Key: demo-secret-key-123
```

## Endpoints

| Method | Route              | Description          |
|--------|--------------------|----------------------|
| GET    | /api/users         | Get all users        |
| GET    | /api/users/{id}    | Get a user by ID     |
| POST   | /api/users         | Create a new user    |
| PUT    | /api/users/{id}    | Update an existing user |
| DELETE | /api/users/{id}    | Delete a user         |
