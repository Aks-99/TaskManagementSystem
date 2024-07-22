using TaskManagement.API.Models.Domain;

namespace TaskManagement.API.Repositories
{
    public interface IEmpTaskRepository
    {
        Task<List<EmpTask>> GetAllAsync();
        Task<EmpTask?> GetByIdAsync(Guid id);
        Task<EmpTask> CreateAsync(EmpTask empTask);
        Task<EmpTask?> UpdateAsync(Guid id, EmpTask empTask);
        Task<EmpTask?> DeleteAsync(Guid id);

        Task<List<EmpTask?>?> GetByEmpNameAsync(string name);
        Task<List<EmpTask>?> GetByStatusAsync(string status);
        Task<List<EmpTask>?> GetByTeamAsync(Guid teamId);

        Task<List<EmpTask>?> GetByDays(int days);
    }
}
