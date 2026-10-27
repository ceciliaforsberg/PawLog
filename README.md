# PawLog

A training log for dogs. Log training sessions, keep track of exercises and follow your dog's progress over time.

I built PawLog as a portfolio project to deepen my skills in ASP.NET Core, Entity Framework Core and Angular, and because I wanted a better way to keep track of training with my own dog.

## Features

- Manage dogs and exercises
- Log training sessions with date, repetitions, reward, rating (1–5) and notes
- Filter training sessions by dog
- Archive exercises instead of deleting them, so training history is never lost
- Angular frontend with a session list and a form for logging new sessions

## Tech stack

| Part | Technology |
| --- | --- |
| Backend | C#, ASP.NET Core Web API (.NET 10) |
| Data access | Entity Framework Core, SQL Server (LocalDB) |
| API documentation | OpenAPI, browsable with Scalar |
| Frontend | Angular (standalone components, signals, Reactive Forms) |
| Tooling | Git, GitHub Issues |

## Project structure

```
PawLog/
├── PawLog.Api/       # Controllers, startup and configuration
├── PawLog.Services/  # Business logic, DTOs (requests and responses), service interfaces
├── PawLog.Data/      # DbContext and EF Core migrations
├── PawLog.Models/    # Entity classes (Dog, Exercise, TrainingSession)
└── pawlog-web/       # Angular frontend
```

The backend follows a layered architecture: `Api` → `Services` → `Data` → `Models`. Each layer only depends on the layers below it.

## Design decisions

- **Request and response records** – the API never exposes entities directly. Incoming data uses request records and outgoing data uses response records, organised by feature (`Services/Requests/Exercises`, `Services/Responses/Dogs` and so on).
- **Soft delete for exercises** – deleting an exercise archives it instead. Archived exercises are hidden from the list and can't be used in new sessions, but existing sessions keep them. `DeleteBehavior.Restrict` in the database acts as a safety net.
- **Cascade delete for dogs** – deleting a dog also deletes its training sessions.
- **Dependency injection** – the `DbContext` is injected into controllers, and services are injected into Angular components.
- **Validation of relations** – creating or updating a session returns `400 Bad Request` if the dog or exercise doesn't exist, instead of a database error.
- **Service layer** – controllers only handle HTTP. Business logic lives in services behind interfaces (e.g. `IExerciseService`), registered with dependency injection to keep the code testable.
- **No repository layer** – EF Core's `DbContext` already acts as a repository (`DbSet`) and unit of work (`SaveChanges`), so services use it directly.
- **ServiceResult** – services that can fail in more than one way return a `ServiceResult` that tells the controller whether the result was a success, not found or invalid. The controller maps it to the right status code.

## Getting started

### Prerequisites

- .NET 10 SDK
- SQL Server LocalDB (included with Visual Studio)
- Node.js (LTS) and Angular CLI
- EF Core tools: `dotnet tool install --global dotnet-ef`

### Run the API

```powershell
dotnet ef database update --project PawLog.Data --startup-project PawLog.Api
dotnet run --project PawLog.Api --launch-profile https
```

API documentation is available at `https://localhost:7027/scalar/v1`.

### Run the frontend

```powershell
cd pawlog-web
npm install
ng serve
```

Open `http://localhost:4200`.

## API endpoints

| Method | Endpoint | Description |
| --- | --- | --- |
| GET | `/api/dogs` | List all dogs |
| GET | `/api/dogs/{id}` | Get a dog |
| POST | `/api/dogs` | Create a dog |
| PUT | `/api/dogs/{id}` | Update a dog |
| DELETE | `/api/dogs/{id}` | Delete a dog and its sessions |
| GET | `/api/exercises` | List active exercises (`?includeArchived=true` for all) |
| GET | `/api/exercises/{id}` | Get an exercise |
| POST | `/api/exercises` | Create an exercise |
| PUT | `/api/exercises/{id}` | Update an exercise |
| DELETE | `/api/exercises/{id}` | Archive an exercise |
| GET | `/api/trainingsessions` | List sessions, newest first (`?dogId=` to filter) |
| GET | `/api/trainingsessions/{id}` | Get a session |
| POST | `/api/trainingsessions` | Log a session |
| PUT | `/api/trainingsessions/{id}` | Update a session |
| DELETE | `/api/trainingsessions/{id}` | Delete a session |

## Roadmap

Planned work is tracked in [GitHub Issues](../../issues). Next up:

- Input validation
- Unit and integration tests (xUnit)
- Authentication with ASP.NET Core Identity and JWT
- Progress view per exercise over time
- Docker, CI with GitHub Actions and deployment to Azure
