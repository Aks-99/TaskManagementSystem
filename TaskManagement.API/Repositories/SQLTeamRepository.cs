using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Data;
using TaskManagement.API.Models.Domain;

namespace TaskManagement.API.Repositories
{
    public class SQLTeamRepository : ITeamRepository
    {
        private readonly TaskManagementSystemDbContext dbContext;

        public SQLTeamRepository(TaskManagementSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        

        public async Task<List<Team>> GetAllAsync()
        {
            return await dbContext.Teams.ToListAsync();
        }

        public async Task<Team?> GetByIdAsync(Guid id)
        {
            return await dbContext.Teams.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Team> CreateAsync(Team team)
        {
            await dbContext.Teams.AddAsync(team);
            await dbContext.SaveChangesAsync();

            return team;
        }


        public async Task<Team?> UpdateAsync(Guid id, Team team)
        {
            var existingTeam = await dbContext.Teams.FirstOrDefaultAsync(x => x.Id == id);

            if (existingTeam == null)
            { return null; }

            existingTeam.Name = team.Name;
            existingTeam.LeadName = team.LeadName;

            await dbContext.SaveChangesAsync();

            return existingTeam;
        }

        public async Task<Team?> DeleteAsync(Guid id)
        {
            var existingTeam = await dbContext.Teams.FirstOrDefaultAsync(x => x.Id == id);

            if (existingTeam == null)
            { return null; }

            dbContext.Teams.Remove(existingTeam);
            await dbContext.SaveChangesAsync();

            return existingTeam;
        }
    }
}
