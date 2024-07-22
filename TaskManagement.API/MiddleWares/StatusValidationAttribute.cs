using System.ComponentModel.DataAnnotations;

namespace TaskManagement.API.MiddleWares
{
    // Checks if the Status column in EmpTasks table is in the correct format
    // Used in AddEmpTaskRequestDto and UpdateEmptaskRequestDto
    public class StatusValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string status)
            {
                if (status == "Not Started" || status == "In Progress" || status == "Completed")
                {
                    return ValidationResult.Success; // Valid status
                }
                else
                {
                    return new ValidationResult("Invalid status. Allowed values: Not Started, In Progress, Completed.");
                }
            }

            return new ValidationResult("Invalid input type. Expected a string.");
        }
    }
}
