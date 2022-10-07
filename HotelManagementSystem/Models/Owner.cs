using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
    public class Owner
    {
        [Key]
        [Required]
        public int Owner_Id { get; set; }
        [Required]
        public string Owner_Password { get; set; }
    }
    public class Jwt
    {
        public string key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
    }
}
