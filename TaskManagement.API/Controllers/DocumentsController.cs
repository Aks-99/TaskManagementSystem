using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Models.Domain;
using TaskManagement.API.Models.DTO.DocumentDto;
using TaskManagement.API.Repositories;

namespace TaskManagement.API.Controllers
{
    // https://localhost:portnumber/api/Upload
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentRepository documentRepository;
        private readonly IMapper mapper;

        public DocumentsController(IDocumentRepository documentRepository, IMapper mapper)
        {
            this.documentRepository = documentRepository;
            this.mapper = mapper;
        }

        // GET ALL Documents by EmpTaskId
        // https://localhost:portnumber/api/Documents/EmpTaskId/{EmpTaskId}
        [HttpGet]
        [Route("/api/Documents/EmpTaskId/{empTaskId:Guid}")]
        public async Task<IActionResult> GetByEmpTaskId([FromRoute] Guid empTaskId)
        {
            var documentDomainModel = await documentRepository.GetDocumentsByTask(empTaskId);

            if (documentDomainModel == null) { return NotFound("No documents uploaded for the task."); }

            return Ok(mapper.Map<List<DocumentDto>>(documentDomainModel));
        }


        // POST Upload Documents
        // https://localhost:portnumber/api/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] UploadDocumentRequestDto request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                // Convert DTO to Domain Model
                var documentDomainModel = new Document
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                    EmpTaskId = request.EmptaskId
                };

                // Use repository to upload document
                await documentRepository.Upload(documentDomainModel);

                return Ok(documentDomainModel);
            }

            return Ok();
        }

        private void ValidateFileUpload(UploadDocumentRequestDto request)
        {
            // Allowed file types are: Images, Text files, Documents, Excel sheets, PDF's
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".txt", ".docx", ".pdf", ".xlsx" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported File Type!");
            }

            // Checking if file size is more than 10MB
            if (request.File.Length > 10486760)
            {
                ModelState.AddModelError("file", "Max file size is 10MB!");
            }
        }
    }
}
