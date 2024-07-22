using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.API.Models.Domain
{
    public class Document
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string FileName { get; set; }

        public string FileDescription { get; set; }

        public string FileExtension { get; set; }

        public long FileSizeInBytes { get; set; }

        public string FilePath { get; set; }

        public Guid EmpTaskId { get; set; }


        // Navigation Properties
        // Connecting to Id column of EmpTasks table
        public EmpTask EmpTask { get; set; }
    }
}
