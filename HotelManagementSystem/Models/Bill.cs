using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HotelManagementSystem.Models
{
    public class Bill
    {
        [Key]
        public int Bill_Id { get; set; }
        [Required]
        public float Bill_Amount { get; set; }
        [Required]
        public DateTime Bill_Date  { get; set; }
        [Required]
        [Display(Name = "Reservation")]
        public virtual int Reservation_Id { get; set; }
        [JsonIgnore]
        [ForeignKey("Reservation_Id")]
        public virtual Room_Reservation Reservation { get; set; }
        [Required]
        public int Guest_Id { get; set; }

    }
}
