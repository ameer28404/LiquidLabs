# LiquidLabs

This repository implements a small ASP.NET Core Web API that fetches posts from an external JSON API, caches them in a local SQL Server table, and exposes endpoints to read posts.

---

## Frameworks & Libraries Used — and Why

- **.NET 9 / C# 13**  
  Targeting the latest version ensures access to the newest language and framework features.

- **IHttpClientFactory / System.Net.Http** (built-in)  
  The service uses `IHttpClientFactory` to create `HttpClient` instances. This is the recommended approach in ASP.NET Core for outbound HTTP calls.

- **Microsoft.Data.SqlClient**  
  Used for direct SQL access.  
  - **Reason:** Simple and transparent. The SQL statements clearly show what’s being stored and retrieved.  
  - **Note:** No ORM is used.

- **ElmahCore**  
  Used for unhandled exception logging to files.  
  - Lightweight and easy to set up.  
  - Logs errors to the `App_Data/ErrLogs` folder.  
  - Includes a simple UI endpoint at `/elmah` for viewing logs.

- **Swagger**  
  Improves discoverability and allows manual testing without extra tools.  
  - Access Swagger at: `/swagger`  
  - Available only in the Development environment.

- **Dependency Injection + Repository / Service Patterns**  
  Uses ASP.NET Core’s built-in DI to register:
  - `IPostRepository` — handles data access using SQL queries.  
  - `IPostService` — handles business logic and caching.  
  This separation improves testability and makes the code easy to extend.

---

## Configuration

Important settings (in `appsettings.json`):

- **ConnectionStrings:DefaultConnection** — SQL Server connection string used to access the database.  
- **ExternalApiBaseUrl** — Base URL for the external posts API.

Example `appsettings.json` snippet:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=FakePostDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;"
  },
  "ExternalApiBaseUrl": "https://jsonplaceholder.typicode.com"
}
```
---

## 🚀 How to Run the Application

### 1️⃣ Create the Database and Table

- **Option 1 – Using Visual Studio:**  
  Use the **SQL Schema Compare** tool to create the database and tables automatically.

- **Option 2 – Using SQL Script:**  
  Run the script file located at `Database/Post.sql` in your SQL Server instance.

### 2️⃣ Configure the Application
Open the `appsettings.json` file and update your database connection string.
Example `appsettings.json` snippet:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=FakePostDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;"
  },
  "ExternalApiBaseUrl": "https://jsonplaceholder.typicode.com"
}
```

### 3️⃣ Build the API
Open a terminal in the project folder (where the `.csproj` file is located) and run:
```bash
dotnet build
```
This will restore dependencies and compile the project.

### 4️⃣ Run the API
To start the Web API, run:
```bash
dotnet run
```
By default, the API will be available at:
  - `https://localhost:5001`
  - `http://localhost:5000`

### 5️⃣ Test the Endpoints
Get all posts
```bash
curl http://localhost:5000/api/posts
```
Get a single post by ID
```bash
curl http://localhost:5000/api/posts/1
```
Behavior:
If the data already exists in the database, it is retrieved from there.
If not, it is fetched from the external API, stored in the database, and then returned to the client.

### 6️⃣ View Swagger UI (Development Environment Only)
If running in the **Development** environment, open:
```bash
https://localhost:5001/swagger
```
This provides an interactive interface to test all endpoints.
