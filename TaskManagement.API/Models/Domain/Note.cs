namespace TaskManagement.API.Models.Domain
{
    public class Note
    {
        public Guid Id { get; set; }

        public string Comments { get; set; }

        public Guid EmpTaskId { get; set; }

        // Navigation Properties
        // Connecting to Id column of EmpTasks table
        public EmpTask EmpTask { get; set; }
    }
}
