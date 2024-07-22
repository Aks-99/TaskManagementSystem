namespace TaskManagement.API.Models.DTO.EmpTaskDto
{
    public class EmpTaskDto
    {
        public Guid Id { get; set; }
        public string Details { get; set; }

        public string AssignedTo { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Status { get; set; }

        public Guid TeamId { get; set; }
    }
}
