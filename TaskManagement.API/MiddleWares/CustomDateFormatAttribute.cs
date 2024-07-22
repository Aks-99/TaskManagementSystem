using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TaskManagement.API.MiddleWares
{
    // Checks if the StartDate and EndDate in EmpTasks table are in the correct format
    // Used in AddEmpTaskRequestDto and UpdateEmptaskRequestDto
    public class CustomDateFormatAttribute : ValidationAttribute
    {
        private const string DateFormat = "dd/MM/yyyy";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string inputDate)
            {
                if (DateTime.TryParseExact(inputDate, DateFormat, null, DateTimeStyles.None, out _))
                {
                    return ValidationResult.Success; // Valid date
                }
                else
                {
                    return new ValidationResult($"Invalid date format. Expected format: {DateFormat}");
                }
            }

            return new ValidationResult("Invalid input type. Expected a string.");
        }
    }

}
