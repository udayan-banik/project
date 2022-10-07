using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HotelManagementSystem.Models
{
    public class AddUpdatePaymentDetailsWithoutId
    {
        [Required]
        public long Payment_Card { get; set; }
        [Required]
        public string Payment_Card_Holder_Name { get; set; }
        [Required]
        [Display(Name = "Bill")]
        public virtual int Bill_Id { get; set; }

        [ForeignKey("Bill_Id")]
        public virtual Bill Bill { get; set; }
    }
}
