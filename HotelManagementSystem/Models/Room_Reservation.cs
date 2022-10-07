using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HotelManagementSystem.Models
{
    public class Room_Reservation
    {
        [Key]
        public int Reservation_Id { get; set; }

        [Required]
        public DateTime Resevation_Check_In { get; set; }
        [Required]
        public DateTime Resevation_Check_Out { get; set; }
        [Required]
        public int Reservation_No_of_Guests { get; set; }

        public Boolean Reservation_Status { get; set; }

        [Required]
        [Display(Name = "Guest")]
        public int Guest_Id { get; set; }

        [JsonIgnore]
        [ForeignKey("Guest_Id")]
        public virtual Guest Guest { get; set; }

        [Required]
        [Display(Name = "Room")]
        public int Room_Id { get; set; }
        [JsonIgnore]
        [ForeignKey("Room_Id")]
        public virtual Room Room { get; set; }

    }
}
