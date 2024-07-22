namespace TaskManagement.API.Models.Domain
{
    public class EmpTask
    {
        public Guid Id { get; set; }

        public string Details { get; set; }

        public string AssignedTo { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Status { get; set; }

        public Guid TeamId { get; set; }

        // Navigation Properties
        // Connecting to Id column of Teams table
        public Team Team { get; set; }
    }
}
