# 🧪 xUnit Testing - Contacts Manager API

This project is a **Contacts Manager Web API** built using **ASP.NET Core** with a strong focus on **xUnit Unit Testing**.  
The goal is to demonstrate writing clean, testable code and verifying it using xUnit tests.

---

## 📚 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [How to Run](#how-to-run)
- [Running Unit Tests](#running-unit-tests)
- [Author](#author)

---

## 📌 Overview

The project manages countries and persons (contacts), providing full CRUD operations with validations.  
Each operation is covered with **xUnit** unit tests, ensuring code reliability and correctness.

---

## ✅ Features

### Country Management
- Add Country ✅
- Get All Countries ✅
- Get Country by ID ✅

### Person Management
- Add Person (with validations) ✅
- Get Person by ID ✅
- Get All Persons ✅
- Get Filtered Persons ✅
- Get Sorted Persons ✅
- Update Person ✅
- Delete Person ✅

All features are tested with **xUnit test cases** (Happy & Unhappy paths).

---

## 🛠 Tech Stack

- **ASP.NET Core Web API**
- **xUnit** for unit testing
- **FluentAssertions** (optional for assertions)
- **TestOutputHelper** for test output tracking
- **C# 10 / .NET 6 or later**

---

## 🗂 Project Structure

/xUnit
│
├── Controllers
│ └── CountryController.cs
│ └── PersonController.cs
│
├── Models
│ └── DTOs / Entities / ViewModels
│
├── Services
│ └── Interfaces & Implementations
│
├── Tests
│ └── xUnit test files per feature
│ └── CountryTests
│ └── PersonTests
│
└── Program.cs / Startup.cs


---

## ▶️ How to Run

1. Clone the repo:
   ```bash
   git clone https://github.com/MuhammedReda263/xUnit.git
   cd xUnit

dotnet restore
dotnet run
dotnet test
