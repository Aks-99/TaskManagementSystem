namespace TaskManagement.API.Models.DTO.NoteDto
{
    public class UpdateNoteRequestDto
    {
        public string Comments { get; set; }

        public Guid EmpTaskId { get; set; }
    }
}
