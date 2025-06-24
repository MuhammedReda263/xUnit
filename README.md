# xUnit-ContactsManager-TestingGuide  
A simple ASP.NET Core project with full CRUD operations and comprehensive xUnit test coverage.

# Contacts Manager - xUnit Testing Playground

This is a simple **ASP.NET Core Web API** project created to **demonstrate and apply unit testing using xUnit** through real-world examples covering contacts and countries management.

---

## What’s Implemented in This Project

### 1. **xUnit Basics**
- Setup of xUnit in ASP.NET Core project.
- Writing simple and structured unit tests.

### 2. **Country Management Features**
- Add Country
- Get All Countries
- Get Country by ID
- Full xUnit test coverage for each operation.

### 3. **Person Management Features**
- Add Person with model validation.
- Get Person by ID
- Get All Persons
- Get Filtered Persons
- Get Sorted Persons
- Update Person
- Delete Person
- Each action includes both **implementation** and **xUnit test** to verify behavior.

### 4. **Validation Handling**
- Model validation for Add/Update Person operations.
- xUnit tests for invalid inputs and edge cases.

### 5. **TestOutputHelper**
- Use of `ITestOutputHelper` for better visibility of test execution and debugging.

---

## Technologies Used
- ASP.NET Core Web API
- C#
- .NET 6+
- xUnit Testing Framework
- Visual Studio 2022 / VS Code

---

## Project Structure
- `Controllers/` → API endpoints for Person & Country
- `Models/` → Entities, DTOs, and Validation Rules
- `Services/` → Business logic and data operations
- `Tests/` → xUnit test classes per feature
- `Program.cs` → App configuration and service registration

---

## How to Run

```bash
git clone https://github.com/MuhammedReda263/xUnit.git
cd xUnit
dotnet restore
dotnet run
