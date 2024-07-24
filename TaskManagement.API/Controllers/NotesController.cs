using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Models.Domain;
using TaskManagement.API.Models.DTO.NoteDto;
using TaskManagement.API.Repositories;

namespace TaskManagement.API.Controllers
{

    // https://localhost:portnumber/api/notes
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly INoteRepository noteRepository;

        public NotesController(IMapper mapper, INoteRepository noteRepository)
        {
            this.mapper = mapper;
            this.noteRepository = noteRepository;
        }

        // GET ALL NOTES
        // GET: https://localhost:portnumber/api/notes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notesDomainModel = await noteRepository.GetAllAsync();

            return Ok(mapper.Map<List<NoteDto>>(notesDomainModel));
        }

        // GET SINGLE NOTE (By Id)
        // GET: https://localhost:portnumber/api/notes/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var notesDomainModel = await noteRepository.GetByIdAsync(id);

            if (notesDomainModel == null) { return NotFound(); }

            return Ok(mapper.Map<NoteDto>(notesDomainModel));
        }

        // GET all NOTE (By EmpTaskId)
        // GET: https://localhost:portnumber/api/notes/emptasks/{empTaskId}
        [HttpGet]
        [Route("/api/Notes/EmpTasks/{empTaskId:guid}")]
        public async Task<IActionResult> GetByEmpTask([FromRoute] Guid empTaskId)
        {
            var noteDomainModel = await noteRepository.GetByEmpTaskIdAsync(empTaskId);

            if (noteDomainModel == null) { return NotFound(); }

            return Ok(mapper.Map<List<NoteDto>>(noteDomainModel));
        }

        // POST TO CREATE NEW NOTE
        // POST: https://localhost:portnumber/api/notes
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddNoteRequestDto addNoteRequestDto)
        {
            var notesDomainModel = mapper.Map<Note>(addNoteRequestDto);

            notesDomainModel = await noteRepository.CreateAsync(notesDomainModel);

            var noteDto = mapper.Map<NoteDto>(notesDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = noteDto.Id }, noteDto);
        }

        // PUT To update Note
        // PUT: https://localhost:portnumber/api/notes/{id}
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateNoteRequestDto updateNoteRequestDto)
        {
            var notesDomainModel = mapper.Map<Note>(updateNoteRequestDto);

            notesDomainModel = await noteRepository.UpdateAsync(id, notesDomainModel);

            if (notesDomainModel == null) { return NotFound(); }

            return Ok(mapper.Map<NoteDto>(notesDomainModel));
        }

        // DELETE To Delete Note
        // DELETE: https://localhost:portnumber/api/notes/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var notesDomainModel = await noteRepository.DeleteAsync(id);

            if (notesDomainModel == null) { return NotFound(); }

            return Ok(mapper.Map<NoteDto>(notesDomainModel));
        }
    }
}
