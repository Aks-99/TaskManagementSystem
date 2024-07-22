using TaskManagement.API.Models.Domain;

namespace TaskManagement.API.Repositories
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetAllAsync();

        Task<Team?> GetByIdAsync(Guid id);

        Task<Team> CreateAsync(Team team);

        Task<Team?> UpdateAsync(Guid id, Team team);

        Task<Team?> DeleteAsync(Guid id);
    }
}
