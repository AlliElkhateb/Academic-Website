using System.ComponentModel.DataAnnotations;

namespace WebAppRepositoryWithUOW.Core.CustomValidation
{
    internal class DevidByNumber : ValidationAttribute
    {
        public int Number { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Student? student = validationContext.ObjectInstance as Student;
            //student.Age
            int? input = value as int?;
            if (input is not null)
            {
                if (input.Value % Number == 0)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("error message");
            }
            return new ValidationResult("error message");
        }
    }
}
