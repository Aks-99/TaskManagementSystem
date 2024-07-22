using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.API.Data;
using TaskManagement.API.Models.Domain;

namespace TaskManagement.API.Repositories
{
    public class SQLEmpTaskRepository : IEmpTaskRepository
    {
        private readonly TaskManagementSystemDbContext dbContext;

        public SQLEmpTaskRepository(TaskManagementSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<EmpTask>> GetAllAsync()
        {
            return await dbContext.EmpTasks.ToListAsync();
        }

        public async Task<EmpTask?> GetByIdAsync(Guid id)
        {
            return await dbContext.EmpTasks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<EmpTask>?> GetByEmpNameAsync(string name)
        {
            List<EmpTask>? empTasksByName = new List<EmpTask>();

            var empTasks = dbContext.EmpTasks;

            foreach (var empTask in empTasks)
            {
                if (empTask.AssignedTo == name) { empTasksByName.Add(empTask); }
            }

            if (empTasksByName.IsNullOrEmpty()) { return null; }

            return empTasksByName;
        }

        public async Task<List<EmpTask>?> GetByStatusAsync(string status)
        {
            List<EmpTask>? empTasksByStatus = new List<EmpTask>();

            var empTasks = dbContext.EmpTasks;

            foreach (var empTask in empTasks)
            {
                if (empTask.Status == status) { empTasksByStatus.Add(empTask); }
            }

            if (empTasksByStatus.IsNullOrEmpty()) { return null; }

            return empTasksByStatus;

        }

        public async Task<List<EmpTask>?> GetByTeamAsync(Guid teamId)
        {
            List<EmpTask>? empTasksByTeam = new List<EmpTask>();

            var empTasks = dbContext.EmpTasks;

            foreach (var empTask in empTasks)
            {
                if (empTask.TeamId == teamId) { empTasksByTeam.Add(empTask); }
            }

            if (empTasksByTeam.IsNullOrEmpty()) { return null; }

            return empTasksByTeam;
        }

        public async Task<List<EmpTask>?> GetByDays(int days)
        {
            List<EmpTask>? empTasksByDate = new List<EmpTask>();

            var empTasks = dbContext.EmpTasks;

            DateOnly today = DateOnly.Parse((DateTime.Now).ToShortDateString());
            DateOnly dueDate = DateOnly.Parse(((DateTime.Now).AddDays(days)).ToShortDateString());

            foreach (var empTask in empTasks)
            {
                var taskEndDate = DateOnly.Parse(empTask.EndDate);

                if (empTask.Status != "Completed" && taskEndDate <= dueDate) { empTasksByDate.Add(empTask); }
            }

            if (empTasksByDate.IsNullOrEmpty()) { return null;  }

            return empTasksByDate;
        }

        public async Task<EmpTask> CreateAsync(EmpTask empTask)
        {
            await dbContext.EmpTasks.AddAsync(empTask);
            await dbContext.SaveChangesAsync();
            return empTask;
        }

        public async Task<EmpTask?> UpdateAsync(Guid id, EmpTask empTask)
        {
            var existingEmpTask = await dbContext.EmpTasks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingEmpTask == null) { return null; }


            existingEmpTask.Details = empTask.Details;
            existingEmpTask.AssignedTo = empTask.AssignedTo;
            existingEmpTask.StartDate = empTask.StartDate;
            existingEmpTask.EndDate = empTask.EndDate;
            existingEmpTask.Status = empTask.Status;
            existingEmpTask.TeamId = empTask.TeamId;

            await dbContext.SaveChangesAsync();

            return existingEmpTask;
        }
        public async Task<EmpTask?> DeleteAsync(Guid id)
        {
            var existingEmpTask = await dbContext.EmpTasks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingEmpTask == null) { return null; }

            dbContext.EmpTasks.Remove(existingEmpTask);
            await dbContext.SaveChangesAsync();

            return existingEmpTask;
        }

    }
}
