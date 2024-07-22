using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.API.Models.DTO.DocumentDto
{
    public class DocumentDto
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
    }
}
