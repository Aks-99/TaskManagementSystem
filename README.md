# TaskManagementSystem

Project Overview:
This is a Task management System which will help employees track their respective tasks and provide actions like attaching documents, adding notes and adding the status for the tasks.

Project Structure:
├── Properties
│   └── launchSettings.json
├── Controllers
│   └── DocumentsController.cs
│   └── EmpTasksController.cs
│   └── NotesController.cs
│   └── TeamsController.cs
├── CustomActionFilters
│   └── ValidateModelAttribute.cs
├── Data
│   └── TaskManagementSystemDbContext.cs
├── Documents
│   └── TechnicalAssignmentDoc.cs
│   └── Test File.txt
├── Logs
│   └── TaskManagementApi_Logs
├── Mappings
│   └── AutoMapperProfiles.cs
├── Middlewares
│   └── CustomDateFormatAttribute.cs
│   └── CustomExceptionHandlerMiddleware.cs
│   └── StatusValidationAttribute.cs
├── Migrations
├── Models
│   └── Domain
│       └── Document.cs
│       └── EmpTask.cs
│       └── Note.cs
│       └── Team.cs
│   └── DTO
│       └── DocumentDto
│           └── DocumentDto.cs
│           └── UploadDocumentRequestDto.cs
│       └── EmpTaskDto
│           └── AddEmpTaskRequestDto.cs
│           └── EmpTaskDto.cs
│           └── UpdateEmpTaskRequestDto.cs
│       └── NoteDto
│           └── AddNoteRequestDto.cs
│           └── EmpNoteDto.cs
│           └── UpdateNoteRequestDto.cs
│       └── TeamDto
│           └── AddTeamRequestDto.cs
│           └── TeamDto.cs
│           └── UpdateTeamRequestDto.cs
├── Repositories
│   └── IDocumentRepository.cs
│   └── IEmpTaskRepository.cs
│   └── INoteRepository.cs
│   └── ITeamRepository.cs
│   └── LocalDocumentRepository.cs
│   └── SQLEmpTaskRepository.cs
│   └── SQLNoteRepository.cs
│   └── SQLTeamRepsoitory.cs
├── appsettings.json
├── Program.cs

Dependencies:
> AutoMapper (13.0.1)
> Microsoft.Entity.Framework.Core (8.0.7)
> Microsoft.Entity.Framework.Core.SQL.Server (8.0.7)
> Microsoft.Entity.Framework.Core.Tools (8.0.7)
> Serilog (4.0.0)
> Serilog.AspNetCore(8.0.1)
> Serilog.Sinks.Console(6.0.0)
> Serilog.Sinks.File(6.0.0)
> swashbuckle.AspNetCore (6.4.0)






