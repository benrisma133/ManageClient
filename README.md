# ManageClient 👥

A professional WPF Client Management desktop application built with C# and .NET 8,
following a clean 3-Tier Architecture with SQLite database and full CI/CD pipeline.

![CI](https://github.com/benrisma133/ManageClient/actions/workflows/ci.yml/badge.svg)

---

## ✨ Features

- Full CRUD — Add, Update, Delete, Get clients
- Duplicate protection by Phone and Email
- Auto-created SQLite database on first run
- Clean AddNew/Update mode switching with enMode pattern
- Automated build, test and publish pipeline with GitHub Actions

---

## 🏗 Architecture

```
Presentation Layer  →  WPF UI
        ↓
Service Layer       →  Business Logic + enMode
        ↓
Repository Layer    →  SQLite + CRUD Operations
```

---

## 🛠 Tech Stack

| Technology | Purpose |
|------------|---------|
| C# / .NET 8 | Core language |
| WPF | Desktop UI framework |
| SQLite | Local database |
| Microsoft.Data.Sqlite | SQLite driver |
| xUnit | Unit testing |
| GitHub Actions | CI/CD pipeline |

---

## 📁 Project Structure

```

ManageClient/
├── .github/
│   └── workflows/
│       └── ci.yml                    ← CI/CD pipeline
├── ManageClient.Repository/          ← SQLite + Models + CRUD
│   ├── Models/Client.cs
│   ├── DatabaseHelper.cs
│   └── ClientRepository.cs
├── ManageClient.Service/             ← Business logic + enMode
│   └── ClientService.cs
├── ManageClient.Presentation/        ← WPF UI
│   ├── MainWindow.xaml
│   └── MainWindow.xaml.cs
├── ManageClient.Tests/               ← xUnit tests
│   └── ClientServiceTests.cs
└── ManageClient.slnx

```

---

## ⚙️ CI/CD Pipeline

Every push to `main` automatically:

1. ✅ Builds all 4 projects
2. ✅ Runs all xUnit tests against an isolated test database
3. ✅ Publishes a standalone `.exe`
4. ✅ Uploads it as a downloadable artifact

---

## 🧪 Tests

| Test | What it verifies |
|------|-----------------|
| `Save_AddNew_ReturnsTrue_AndSwitchesToUpdateMode` | Add succeeds and mode switches to Update |
| `Save_AddNew_AssignsNewId` | New client gets a valid ID from database |
| `Save_AddNew_FailsIfPhoneAlreadyExists` | Duplicate phone is rejected |
| `Save_AddNew_FailsIfEmailAlreadyExists` | Duplicate email is rejected |
| `Save_Update_ReturnsTrueAfterSuccessfulAdd` | Update works after AddNew |
| `Delete_ExistingClient_ReturnsTrue` | Delete returns true for existing client |
| `GetAll_ReturnsAllAddedClients` | GetAll returns correct count |
| `GetClientById_ReturnsCorrectClient` | Correct client is returned by ID |

---

## 🗄 Database

SQLite database is created automatically on first run — no setup needed.

```
Client Table
─────────────────────────────────────
Id          INTEGER  PRIMARY KEY AUTOINCREMENT
FullName    TEXT     NOT NULL
PhoneNumber TEXT     NOT NULL  UNIQUE
Email       TEXT     NOT NULL  UNIQUE
─────────────────────────────────────
```

---

## 🚀 How to Run

1. Clone the repository
```bash
   git clone https://github.com/benrisma133/ManageClient.git
```
2. Open `ManageClient.slnx` in Visual Studio 2022
3. Set `ManageClient.Presentation` as startup project
4. Press `F5` to run

---

## 📦 Download

Go to [Actions](https://github.com/benrisma133/ManageClient/actions) → latest run → **Artifacts** → download `ManageClient-win-x64.zip`

---

## 👨‍💻 Author

**Ismail** — [@benrisma133](https://github.com/benrisma133)

