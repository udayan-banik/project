using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
    public class Room
    {
        [Key]
        public int Room_Id { get; set; }
        [Required]
        public Boolean Room_Status { get; set; }
        public string Room_Comment { get; set; }
        public string Room_Inventory { get; set; }
        [Required]
        public float Room_Price { get; set; }
    }
}
