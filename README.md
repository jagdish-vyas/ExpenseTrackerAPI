# Expense Tracker API 💰

A modern, lightweight RESTful API built for managing personal expenses. This project demonstrates backend development best practices, including the implementation of Entity Framework Core with a Code-First approach.

## 🚀 Tech Stack
* **Framework:** .NET 8 (ASP.NET Core Web API)
* **Language:** C#
* **ORM:** Entity Framework Core (EF Core)
* **Database:** SQLite (Lightweight, file-based database)
* **Documentation:** Swagger UI

## ✨ Key Features
* **Full CRUD Operations:** * `GET` - Retrieve all expenses
  * `POST` - Add a new expense
  * `PUT` - Update an existing expense
  * `DELETE` - Remove an expense
* **Code-First Database:** Auto-generation of database and tables using EF Core Migrations.
* **Dependency Injection:** Loosely coupled architecture for `ApplicationDbContext`.

## 🛠️ How to Run Locally
1. Clone the repository.
2. Open the project in VS Code or Visual Studio.
3. Open the terminal and run:
   ```bash
   dotnet run