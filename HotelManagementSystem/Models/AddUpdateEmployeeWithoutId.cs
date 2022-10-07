using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
    public class AddUpdateEmployeeWithoutId:IValidatableObject
    {
        [Required]
        public string Employee_Name { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Employee_Password { get; set; }
        [Required]
        public string Employee_Designation { get; set; }
        [Required]
        public float Employee_Salary { get; set; }
        [Required]
        [EmailAddress]
        public string Employee_Email { get; set; }
        [Required]
        [Range(18, 99, ErrorMessage = "Age should be between 18 to 99")]
        public int Employee_Age { get; set; }
        [Required]
        public long Employee_PhoneNo { get; set; }
        [Required]
        public string Employee_Address { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Employee_PhoneNo.ToString().Length != 10)
            {
                yield return new ValidationResult("Phone no. not correct");
            }
            if(Employee_Password.Length < 8)
            {
                yield return new ValidationResult("Password Should be of 8 characters and 1 uppercase and 1 lowercase");
            }
        }
    }
}
