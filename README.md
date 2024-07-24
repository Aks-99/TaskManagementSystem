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

*****************************************************

Dependencies:
=====================================================
> AutoMapper (13.0.1)
> Microsoft.Entity.Framework.Core (8.0.7)
> Microsoft.Entity.Framework.Core.SQL.Server (8.0.7)
> Microsoft.Entity.Framework.Core.Tools (8.0.7)
> Serilog (4.0.0)
> Serilog.AspNetCore(8.0.1)
> Serilog.Sinks.Console(6.0.0)
> Serilog.Sinks.File(6.0.0)
> swashbuckle.AspNetCore (6.4.0)

*****************************************************

API's

=====================================================
Teams: https://localhost:portnumber/api/teams
Endpoints:
_____________________________________________________
GET: https://localhost:portnumber/api/teams      ->  Returns a list of all teams data from the teams table in the database.
Request parameters: No parameters
Request response: 200 OK
{
  "id": "ab4a8def-998d-4e02-bd60-03af45298028",
  "name": "string",
  "leadName": "string"
},
{
  "id": "458f9c61-4012-420a-b50d-28751b1fdadc",
  "name": "string",
  "leadName": "string"
},
{
  "id": "ee533d69-b942-4c40-a749-ae507810f418",
  "name": "string",
  "leadName": "string"
}]
_____________________________________________________
GET: https://localhost:portnumber/api/teams/{id}      ->  Returns a single team by id from the teams table in the database.
Request parameters: 
{ "id": "guid" } (*required)
Request response: 200 OK
{
  "id": "ab4a8def-998d-4e02-bd60-03af45298028",
  "name": "string",
  "leadName": "string"
}
_____________________________________________________
POST: https://localhost:portnumber/api/teams      ->  Create a new team and add it into the database in the teams table.
Request parameters: 
{
  "name" : " string",
  "leadName" : "string"
}
Request Response: 201 CREATED
{
  "id": "358054bf-3369-4c47-d0a5-08dcab9f4a4b",
  "name": "string",
  "leadName": "string"
}
_____________________________________________________
PUT: https://localhost:portnumber/api/teams/{id}      ->  Update single team data by id from database
Request parameters: 
{ "id": "guid" } (*required)
{
  "name": "string",
  "leadName": "string"
}
Request Response: 200 OK
{
  "id": "358054bf-3369-4c47-d0a5-08dcab9f4a4b",
  "name": "string",
  "leadName": "string"
}
_____________________________________________________
DELETE: https://localhost:portnumber/api/teams/{id}      ->  Delete single team data by id from database
Request parameters: 
{ "id": "guid" } (*required)
Request Response: 200 OK
{
  "id": "358054bf-3369-4c47-d0a5-08dcab9f4a4b",
  "name": "string",
  "leadName": "string"
}

=====================================================
Notes: https://localhost:portnumber/api/notes
Endpoints:
_____________________________________________________
GET: https://localhost:portnumber/api/notes      ->  Returns a list of all notes data from the notes table in the database.
Request parameters: No parameters
Request response: 200 OK
{
  "id": "79d1ee04-0291-4061-8478-39da12403a7e",
  "comments": "string",
  "empTaskId": "97f78af3-35a0-4040-b69d-1b681193204c"
},
{
  "id": "b0454208-0431-4f30-bd2b-e62e1d27abcc",
  "comments": "string",
  "empTaskId": "78170408-08ff-4c07-9e03-0afa24f7a751"
},
{
  "id": "a7a99320-efac-4150-af4e-fe9d55c9b844",
  "comments": "string",
  "empTaskId": "92b38395-3eac-4c1d-8fcf-a06bca6b193b"
}
_____________________________________________________
GET: https://localhost:portnumber/api/notes/{id}      ->  Returns a single note by id from the notes table in the database.
Request parameters: 
{ "id": "guid" } (*required)
Request response: 200 OK
{
  "id": "79d1ee04-0291-4061-8478-39da12403a7e",
  "comments": "string",
  "empTaskId": "97f78af3-35a0-4040-b69d-1b681193204c"
}
_____________________________________________________
GET: https://localhost:portnumber/api/notes/EmpTasks/{empTaskId}      ->  Returns all notes with the empTaskId from the notes table in the database.
Request parameters: 
{ "empTaskId": "guid" } (*required)
Request response: 200 OK
{
  "id": "b0454208-0431-4f30-bd2b-e62e1d27abcc",
  "comments": "string",
  "empTaskId": "97f78af3-35a0-4040-b69d-1b681193204c"
},
{
  "id": "a7a99320-efac-4150-af4e-fe9d55c9b844",
  "comments": "string",
  "empTaskId": "97f78af3-35a0-4040-b69d-1b681193204c"
}
_____________________________________________________
POST: https://localhost:portnumber/api/notes      ->  Create a new note and add it into the database in the notes table.
Request parameters: 
{
  "comments": "string",
  "empTaskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
Request Response: 201 CREATED
{
  "id": "a7a99320-efac-4150-af4e-fe9d55c9b565",
  "comments": "string",
  "empTaskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
_____________________________________________________
POST: https://localhost:portnumber/api/notes/{id}      ->  Update a single note by id from database.
Request parameters: 
{ "id": "guid" } (*required)
{
  "comments": "string",
  "empTaskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
Request Response: 200 OK
{
  "id": "a7a99320-efac-4150-af4e-fe9d55c9b565",
  "comments": "string",
  "empTaskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
_____________________________________________________
DELETE: https://localhost:portnumber/api/notes/{id}      ->  Delete a single note by id from database.
Request parameters: 
{ "id": "guid" } (*required)
Request Response: 200 OK
{
  "id": "a7a99320-efac-4150-af4e-fe9d55c9b565",
  "comments": "string",
  "empTaskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
_____________________________________________________













