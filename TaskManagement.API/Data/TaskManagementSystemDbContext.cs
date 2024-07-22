using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Models.Domain;

namespace TaskManagement.API.Data
{
    public class TaskManagementSystemDbContext : DbContext
    {
        public TaskManagementSystemDbContext(DbContextOptions<TaskManagementSystemDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Team> Teams { get; set; }

        public DbSet<EmpTask> EmpTasks { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<Document> Documents { get; set; }


        // Seeding data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Data for Team
            // Back-End, Front-End, Testing
            var team = new List<Team>()
            {
                new Team()
                {
                    Id = Guid.Parse("458f9c61-4012-420a-b50d-28751b1fdadc"),
                    Name = "Back-End",
                    LeadName = "Back-End Lead"
                },
                new Team()
                {
                    Id = Guid.Parse("ee533d69-b942-4c40-a749-ae507810f418"),
                    Name = "Front-End",
                    LeadName = "Front-End Lead"
                },
                new Team()
                {
                    Id = Guid.Parse("ab4a8def-998d-4e02-bd60-03af45298028"),
                    Name = "Testing",
                    LeadName = "Test Lead"
                }
            };
            // Seed teams to the database
            modelBuilder.Entity<Team>().HasData(team);


            // Seed data for EmployeeTasks
            var employeeTasks = new List<EmpTask>()
            {
                new EmpTask()
                {
                    Id = Guid.Parse("97f78af3-35a0-4040-b69d-1b681193204c"),
                    Details = "Back-End Development",
                    AssignedTo = "Back-End developer",
                    StartDate = "01/01/2024",
                    EndDate = "01/03/2024",
                    Status = "In Progress",
                    TeamId = Guid.Parse("458f9c61-4012-420a-b50d-28751b1fdadc")
                },
                new EmpTask()
                {
                    Id = Guid.Parse("78170408-08ff-4c07-9e03-0afa24f7a751"),
                    Details = "Front-End Development",
                    AssignedTo = "Front-End developer",
                    StartDate = "01/02/2024",
                    EndDate = "01/04/2024",
                    Status = "Not Started",
                    TeamId = Guid.Parse("ee533d69-b942-4c40-a749-ae507810f418")
                },
                new EmpTask()
                {
                    Id = Guid.Parse("92b38395-3eac-4c1d-8fcf-a06bca6b193b"),
                    Details = "Testing tasks",
                    AssignedTo = "Tester",
                    StartDate = "01/02/2024",
                    EndDate = "01/05/2024",
                    Status = "Not Started",
                    TeamId = Guid.Parse("ab4a8def-998d-4e02-bd60-03af45298028")
                }
            };
            // Seed EmployeeTask to the database
            modelBuilder.Entity<EmpTask>().HasData(employeeTasks);


            // Seed data for Notes
            var notes = new List<Note>()
            {
                new Note()
                {
                    Id = Guid.Parse("79d1ee04-0291-4061-8478-39da12403a7e"),
                    Comments = "Back end development has started.",
                    EmpTaskId = Guid.Parse("97f78af3-35a0-4040-b69d-1b681193204c")
                },
                new Note()
                {
                    Id = Guid.Parse("b0454208-0431-4f30-bd2b-e62e1d27abcc"),
                    Comments = "Front end development has not started.",
                    EmpTaskId = Guid.Parse("78170408-08ff-4c07-9e03-0afa24f7a751")
                },
                new Note()
                {
                    Id = Guid.Parse("a7a99320-efac-4150-af4e-fe9d55c9b844"),
                    Comments = "Testing has not started.",
                    EmpTaskId = Guid.Parse("92b38395-3eac-4c1d-8fcf-a06bca6b193b")
                }
            };
            // Seed Note to the database
            modelBuilder.Entity<Note>().HasData(notes);

        }
    }
}
