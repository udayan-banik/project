using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
    public class AddUpdateGuestWithoutId:IValidatableObject
    {
        [Required]
        public string Guest_Name { get; set; }
        [Required]
        [EmailAddress]
        public string Guest_Email { get; set; }
        [Range(18, 99, ErrorMessage = "Age should be between 18 to 99")]
        public int Guest_Age { get; set; }
        [Required]
        [Range(0, 9999999999)]
        public long Guest_Phone_Number { get; set; }
        [Required]
        [Range(0, 999999999999)]
        public long Guest_Aadhar_Id { get; set; }
        [Required]
        public string Guest_Address { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Guest_Phone_Number.ToString().Length != 10)
            {
                yield return new ValidationResult("Phone no. not correct");
            }
            if (Guest_Aadhar_Id.ToString().Length != 12)
            {
                yield return new ValidationResult("Aadhar Id not correct");
            }
        }
    }
}
