
# Clubify — Sprints 1 & 2

## Project Overview
Clubify is an ASP.NET Core Blazor Server application for managing clubs and memberships.  
- **Sprint 1** focused on laying the foundation with authentication, role-based access, CRUD operations for clubs, and Bootstrap styling.
- **Sprint 2** expanded functionality to include membership management, success/error notifications, and unit testing.

---

## Getting Started

### Prerequisites
- .NET 8 SDK (or the latest supported version)
- SQL Server (Express or LocalDB recommended)
- Visual Studio 2022+ or VS Code

---

### Setup Steps

1. **Clone the Repository**
   ```bash
   git clone https://github.com/your-username/clubify.git
   cd clubify

2. **Configure the Database**
    ```json
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ClubifyDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}

3. **Apply Migrations**
   ```bash
    dotnet ef database update

4. **Seed the Admin User**
By default, an admin user is seeded via the application startup.

Default credentials (example):
Username: admin@example.com
Password: Admin123!

5. **Run the Application**
   ```bash
    dotnet run

    Navigate to https://localhost:5001.