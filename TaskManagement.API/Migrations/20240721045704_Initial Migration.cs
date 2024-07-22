using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeadName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmpTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpTasks_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_EmpTasks_EmpTaskId",
                        column: x => x.EmpTaskId,
                        principalTable: "EmpTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_EmpTasks_EmpTaskId",
                        column: x => x.EmpTaskId,
                        principalTable: "EmpTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "LeadName", "Name" },
                values: new object[,]
                {
                    { new Guid("458f9c61-4012-420a-b50d-28751b1fdadc"), "Back-End Lead", "Back-End" },
                    { new Guid("ab4a8def-998d-4e02-bd60-03af45298028"), "Test Lead", "Testing" },
                    { new Guid("ee533d69-b942-4c40-a749-ae507810f418"), "Front-End Lead", "Front-End" }
                });

            migrationBuilder.InsertData(
                table: "EmpTasks",
                columns: new[] { "Id", "AssignedTo", "Details", "EndDate", "StartDate", "Status", "TeamId" },
                values: new object[,]
                {
                    { new Guid("78170408-08ff-4c07-9e03-0afa24f7a751"), "Front-End developer", "Front-End Development", "01/04/2024", "01/02/2024", "Not Started", new Guid("ee533d69-b942-4c40-a749-ae507810f418") },
                    { new Guid("92b38395-3eac-4c1d-8fcf-a06bca6b193b"), "Tester", "Testing tasks", "01/05/2024", "01/02/2024", "Not Started", new Guid("ab4a8def-998d-4e02-bd60-03af45298028") },
                    { new Guid("97f78af3-35a0-4040-b69d-1b681193204c"), "Back-End developer", "Back-End Development", "01/03/2024", "01/01/2024", "In Progress", new Guid("458f9c61-4012-420a-b50d-28751b1fdadc") }
                });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Comments", "EmpTaskId" },
                values: new object[,]
                {
                    { new Guid("79d1ee04-0291-4061-8478-39da12403a7e"), "Back end development has started.", new Guid("97f78af3-35a0-4040-b69d-1b681193204c") },
                    { new Guid("a7a99320-efac-4150-af4e-fe9d55c9b844"), "Testing has not started.", new Guid("92b38395-3eac-4c1d-8fcf-a06bca6b193b") },
                    { new Guid("b0454208-0431-4f30-bd2b-e62e1d27abcc"), "Front end development has not started.", new Guid("78170408-08ff-4c07-9e03-0afa24f7a751") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_EmpTaskId",
                table: "Documents",
                column: "EmpTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpTasks_TeamId",
                table: "EmpTasks",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_EmpTaskId",
                table: "Notes",
                column: "EmpTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "EmpTasks");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
