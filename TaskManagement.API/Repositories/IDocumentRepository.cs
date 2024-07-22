using TaskManagement.API.Models.Domain;

namespace TaskManagement.API.Repositories
{
    public interface IDocumentRepository
    {
        Task<Document> Upload(Document document);

        Task<List<Document>?> GetDocumentsByTask(Guid taskId);
    }
}
