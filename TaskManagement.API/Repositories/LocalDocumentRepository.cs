using Microsoft.IdentityModel.Tokens;
using TaskManagement.API.Data;
using TaskManagement.API.Models.Domain;

namespace TaskManagement.API.Repositories
{
    public class LocalDocumentRepository : IDocumentRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly TaskManagementSystemDbContext dbContext;

        public LocalDocumentRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, TaskManagementSystemDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<List<Document>?> GetDocumentsByTask(Guid empTaskId)
        {
            List<Document>? docsByTask = new List<Document>();

            var documents = dbContext.Documents;

            foreach (var document in documents)
            {
                if (document.EmpTaskId == empTaskId) { docsByTask.Add(document); }
            }

            if (docsByTask.IsNullOrEmpty()) { return null; }

            return docsByTask;
        }

        public async Task<Document> Upload(Document document)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Documents", $"{document.FileName}{document.FileExtension}");

            // Upload document to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await document.File.CopyToAsync(stream);

            // https://localhost:portnumber/documents/document.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Documents/{document.FileName}{document.FileExtension}";

            document.FilePath = urlFilePath;

            // Add document toDocument table
            await dbContext.Documents.AddAsync(document);
            await dbContext.SaveChangesAsync();

            return document;
        }
    }
}
