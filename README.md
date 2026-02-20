# 🧾 Inventory API

API RESTful simple desarrollada con **.NET 10** para la gestión de inventario, incluyendo autenticación y manejo básico de productos, roles y permisos.

---

## 🚀 Tecnologías utilizadas

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- JWT Authentication
- Scalar (API Reference UI)
- SQL Server / PostgreSQL (configurable)

---

## 📂 Estructura del Proyecto

```
├── 📁 Inventario_API_REST
│   ├── 📁 Controllers
│   │   ├── 📁 Auth
│   │   │   ├── 📄 AuthController.cs
│   │   │   ├── 📄 PermissionController.cs
│   │   │   └── 📄 RoleController.cs
│   │   ├── 📁 Products
│   │   │   └── 📄 ProductsController.cs
│   │   └── 📄 BaseController.cs
│   ├── 📁 Database
│   │   ├── 📁 Models
│   │   │   ├── 📄 BaseEntity.cs
│   │   │   ├── 📄 Permission.cs
│   │   │   ├── 📄 Product.cs
│   │   │   ├── 📄 Role.cs
│   │   │   └── 📄 User.cs
│   │   ├── 📄 DbInitializer.cs
│   │   └── 📄 InventoryDbContext.cs
│   ├── 📁 Extensions
│   │   ├── 📁 Middlewares
│   │   │   ├── 📄 GlobalException.cs
│   │   │   └── 📄 TimerLog.cs
│   │   ├── 📁 Registers
│   │   │   ├── 📄 AuthorizationRegister.cs
│   │   │   ├── 📄 DbContextRegister.cs
│   │   │   ├── 📄 JwtRegister.cs
│   │   │   ├── 📄 MediatRRegister.cs
│   │   │   ├── 📄 OpenApiRegister.cs
│   │   │   └── 📄 RegisterCors.cs
│   │   └── 📄 ClaimsPrincipalExtensions.cs
│   ├── 📁 Features
│   │   ├── 📁 Auth
│   │   │   ├── 📄 AuthLoginCommand.cs
│   │   │   └── 📄 AuthRegisterCommand.cs
│   │   ├── 📁 Permissions
│   │   │   └── 📄 GetPermissionsQuery.cs
│   │   ├── 📁 Products
│   │   │   ├── 📄 CreateProductCommand.cs
│   │   │   ├── 📄 DeleteProductCommand.cs
│   │   │   ├── 📄 GetProductQuery.cs
│   │   │   └── 📄 UpdateProductCommand.cs
│   │   └── 📁 Roles
│   │       └── 📄 GetRolesQuery.cs
│   ├── 📁 Middlewares
│   │   ├── 📄 ExceptionMiddleware.cs
│   │   └── 📄 PerformanceMiddleware.cs
│   ├── 📁 Migrations
│   │   ├── 📄 20260218140214_InitialCreate.Designer.cs
│   │   ├── 📄 20260218140214_InitialCreate.cs
│   │   └── 📄 InventoryDbContextModelSnapshot.cs
│   ├── 📁 Properties
│   │   └── ⚙️ launchSettings.json
│   ├── 📁 Share
│   │   ├── 📁 Constants
│   │   │   ├── 📄 CorsPolicy.cs
│   │   │   ├── 📄 Permissions.cs
│   │   │   └── 📄 Roles.cs
│   │   ├── 📁 MyMediatR
│   │   │   ├── 📄 IHandler.cs
│   │   │   └── 📄 MediatR.cs
│   │   ├── 📁 Result
│   │   │   ├── 📄 AsyncHandler.cs
│   │   │   ├── 📄 Result.cs
│   │   │   └── 📄 ResultPaginated.cs
│   │   └── 📄 Imports.cs
│   ├── ⚙️ .gitignore
│   ├── 📄 Inventario_API_REST.csproj
│   ├── 📄 Inventario_API_REST.csproj.user
│   ├── 📄 Inventario_API_REST.http
│   ├── 📄 Program.cs
│   ├── ⚙️ appsettings.Development.json
│   ├── ⚙️ appsettings.json
│   └── 📄 inventory.db
└── 📄 Inventario_API_REST.slnx
```