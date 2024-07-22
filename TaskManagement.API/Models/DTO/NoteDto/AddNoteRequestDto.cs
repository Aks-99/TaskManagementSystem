namespace TaskManagement.API.Models.DTO.NoteDto
{
    public class AddNoteRequestDto
    {
        public string Comments { get; set; }

        public Guid EmpTaskId { get; set; }
    }
}
