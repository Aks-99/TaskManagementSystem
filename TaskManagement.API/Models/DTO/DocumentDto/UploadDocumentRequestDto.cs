using System.ComponentModel.DataAnnotations;

namespace TaskManagement.API.Models.DTO.DocumentDto
{
    public class UploadDocumentRequestDto
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string FileName { get; set; }

        public string? FileDescription { get; set; }

        [Required]
        public Guid EmptaskId { get; set; }
    }
}
