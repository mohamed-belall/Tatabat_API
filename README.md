# Tatabat_API

Tatabat_API is a scalable ASP.NET Core Web API designed for e-commerce management, supporting operations for products, employees, and departments. The project leverages modern design patterns and technologies to ensure maintainability, flexibility, and performance.

## Features

- ASP.NET Core (.NET 8) Web API architecture
- Repository and Specification patterns for clean, reusable data access
- AutoMapper integration for efficient DTO mapping
- Entity Framework Core for robust database operations and migrations
- Modular service registration for extensibility
- Comprehensive error handling and middleware
- Authentication and identity management using ASP.NET Core Identity
- RESTful endpoints for products, employees, and departments

## Technologies Used

- C# 12
- ASP.NET Core 8
- Entity Framework Core
- AutoMapper
- ASP.NET Core Identity
- SQL Server

## Getting Started

1. **Clone the repository:**
- git clone https://github.com/yourusername/Tatabat_API.git

2. **Configure the database connection:**
- Update the connection string in `appsettings.json` to point to your SQL Server instance.

3. **Apply migrations:**
- dotnet ef database update

4. **Run the API:**
- dotnet run

5. **Access Swagger UI:**
- Navigate to `http://localhost:<port>/swagger` for API documentation and testing.

## API Endpoints

### Products

- `GET /api/Products`  
Retrieves a paginated list of products. 
Supports filtering, sorting, and pagination via query parameters.  
**Response:** `200 OK` with paginated product data.

- `GET /api/Products/{id}` 
Retrieves a single product by its ID.  
**Response:** `200 OK` with product details, or `404 Not Found` if not found.

- `GET /api/Products/Brands` 
Retrieves all product brands.  
**Response:** `200 OK` with a list of brands.

- `GET /api/Products/Categories` 
Retrieves all product categories. 
**Response:** `200 OK` with a list of categories.

### Employees

- `GET /api/Employee`  
Retrieves all employees, including their department details. 
**Response:** `200 OK` with a list of employees, or `404 Not Found` if none exist.

### Error Handling

- All endpoints return standardized error responses using the `ApiResponse` and `ApiValidationErrorResponse` models for consistent error messaging.

### Authentication

- Some endpoints may require authentication. Use a valid JWT token in the `Authorization` header.

### Swagger UI

- Interactive API documentation is available at `/swagger` when running the application.

## Project Structure

- `Talabat.API` – API controllers and DTOs
- `Talabat.Core` – Domain entities, interfaces, and specifications
- `Talabat.Repository` – Data access and identity management
- `Talabat.Service` – Business logic and service registration

## Example Endpoints

- `GET /api/products` – Retrieve all products with filtering, sorting, and pagination
- `GET /api/employees` – Retrieve all employees with department details
- `POST /api/products` – Add a new product
- `POST /api/employees` – Add a new employee

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

## License

This project is licensed under the MIT License.

