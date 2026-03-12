# Property Rental Management System

A full-stack technical assignment for managing properties, tenants, and lease contracts using ASP.NET Core Web API,
SQL Server, EF Core, JWT Authentication, Clean Architecture, and Windows Forms client.

---

## Overview

This project is a Property Rental Management System that provides:

- Property management
- Tenant management
- Lease contract management
- JWT-based authentication
- Swagger/OpenAPI documentation
- Windows Forms desktop client for property operations

---

## Technology Stack

### Backend
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger / OpenAPI

### Frontend
- Windows Forms

### Architecture
- Clean Architecture
  - Domain
  - Application
  - Infrastructure
  - API

---

## Business Rules

The system enforces the following business rules:

1. A property cannot be leased if it is not available.
2. A property cannot have overlapping lease periods.
3. When a lease is created, the property automatically becomes unavailable.

---

## Main Entities

### Property
- Id
- Name
- Address
- City
- MonthlyPrice
- IsAvailable
- CreatedAt

### Tenant
- Id
- FullName
- Phone
- Email
- NationalId

### Lease
- Id
- PropertyId
- TenantId
- StartDate
- EndDate
- MonthlyPrice

### User
- Id
- FullName
- Email
- PasswordHash
- Role
- CreatedAt

---

## Database Migrations

On the Infrastructure project:
- Add-Migration first -Context ApplicationDbContext
- Update-Database -Context ApplicationDbContext

----

## API Endpoints

Authentication:
- POST /api/auth/register
- POST /api/auth/login

Properties:
- GET /api/properties
- GET /api/properties/{id}
- POST /api/properties
- PUT /api/properties/{id}
- DELETE /api/properties/{id}

Tenants:
- GET /api/tenants
- GET /api/tenants/{id}
- POST /api/tenants
- PUT /api/tenants/{id}
- DELETE /api/tenants/{id}

Leases:
- GET /api/leases
- GET /api/leases/{id}
- POST /api/leases
- PUT /api/leases/{id}
- DELETE /api/leases/{id}
- GET /api/leases/property/{propertyId}
- PUT /api/leases/end/{id}


Nationalities:
- GET /api/nationalities
- GET /api/nationalities/{id}
- POST /api/nationalities
- PUT /api/nationalities/{id}
- DELETE /api/nationalities/{id}

----

## Run the project

Backend
- Edit ConnectionStrings section in the appsettings.json
- Eidt launchSettings.json => I use https profile
- Run the API project

Frontend
- Identify ServerIP in ApiRoutes.cs (the link in launchSettings.json)

----

## Project Structure

```text
src/
 ├── API
 ├── Application
 ├── Domain
 ├── Infrastructure
 └── WinForms

---











