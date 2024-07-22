namespace TaskManagement.API.Models.DTO.NoteDto
{
    public class NoteDto
    {
        public Guid Id { get; set; }

        public string Comments { get; set; }

        public Guid EmpTaskId { get; set; }
    }
}
