using TaskManagement.API.MiddleWares;

namespace TaskManagement.API.Models.DTO.EmpTaskDto
{
    public class UpdateEmpTaskRequestDto
    {
        public string Details { get; set; }

        public string AssignedTo { get; set; }

        [CustomDateFormat]
        public string StartDate { get; set; }

        [CustomDateFormat]
        public string EndDate { get; set; }

        [StatusValidation(ErrorMessage = "Invalid status. Allowed values: 'Not Started', 'In Progress', 'Completed'.")]
        public string Status { get; set; }

        public Guid TeamId { get; set; }
    }
}
