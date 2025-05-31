Clubify — Sprint 1
Project Overview
Clubify is an ASP.NET Core Blazor Server application designed for managing clubs and their details. Sprint 1 focused on laying the foundations, including authentication, role-based authorization, CRUD operations for clubs, and a consistent Bootstrap-styled UI.

Getting Started
Prerequisites
.NET 8 SDK (or latest supported version)

SQL Server (Express or LocalDB recommended)

Visual Studio 2022+ or VS Code

Setup Steps
Clone the Repository

bash
Copy
Edit
git clone https://github.com/your-username/clubify.git
cd clubify
Configure the Database

Open appsettings.json (or appsettings.Development.json) and set your database connection string:

json
Copy
Edit
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ClubifyDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
Apply Migrations

bash
Copy
Edit
dotnet ef database update
(Make sure dotnet-ef is installed.)

Seed the Admin User

By default, an admin user is seeded via the application startup.

Check the Data/ApplicationDbContextSeed.cs file or seed logic.

Default credentials (example):

Username: admin@example.com

Password: Admin123!

Run the Application

bash
Copy
Edit
dotnet run
Navigate to https://localhost:5001 (or the port configured).

Features Implemented in Sprint 1
Authentication — ASP.NET Core Identity with login/logout
Admin Role — Only admins can access club management
Club CRUD — Create, Edit, Delete (soft delete), and Details pages
Soft Delete — Clubs are marked inactive instead of permanently deleted
Role-based Authorization — Only admins can manage clubs
Bootstrap UI — Responsive and consistent styling across all pages

Usage Instructions
Login as the admin user to access the Clubs management section (/clubs).

Create a new club using the Create New button.

Edit, delete (soft delete), or view details using the corresponding action buttons.

Soft-deleted clubs will no longer appear in the list.

Known Issues / Next Steps
Success/error notifications are pending (Sprint 2).

README improvements for additional features will continue in future sprints.

Membership Management and user profile pages planned for Sprint 2.

Contribution
Feel free to fork the repository and submit pull requests. For major changes, please open an issue first to discuss what you’d like to change.

Contact
For questions or feedback, please contact the project maintainer:

Name: Dan Wale

Email: danwale74@gmail.com

Additional Notes
This README is tailored to Sprint 1 and will be expanded as the project grows.

For any questions, please reach out!
