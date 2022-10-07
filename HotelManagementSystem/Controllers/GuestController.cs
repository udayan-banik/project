using HotelManagementSystem.Data;
using HotelManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController : Controller
    {
        private readonly HMSApiDbcontext dbContext;
        public GuestController(HMSApiDbcontext dbContext)
        {
            this.dbContext = dbContext;
        }
        #region GetGuests

        [HttpGet]
        [Route("/GetGuests")]
        public IActionResult GetGuest()
        {
            return Ok(dbContext.Guests.ToList());
        }
        #endregion

        #region AddGuest
        [HttpPost]
        [Route("/AddGuest")]
        public IActionResult AddGuest(AddUpdateGuestWithoutId addGuestWithoutId)
        {
            var Guest = new Guest()
            {
                Guest_Aadhar_Id = addGuestWithoutId.Guest_Aadhar_Id,
                Guest_Address = addGuestWithoutId.Guest_Address,
                Guest_Age = addGuestWithoutId.Guest_Age,
                Guest_Email = addGuestWithoutId.Guest_Email,
                Guest_Name = addGuestWithoutId.Guest_Name,
                Guest_Phone_Number = addGuestWithoutId.Guest_Phone_Number,
            };
            dbContext.Guests.Add(Guest);
            dbContext.SaveChanges();

            return Ok("New Guest Added");
        }
        #endregion

        # region UpdateGuest
        [HttpPut]
        [Route("/UpdateGuest")]
        public IActionResult UpdateGuest( int Guestid, AddUpdateGuestWithoutId updateGuestWithoutId)
        {
            var CheckGuest = dbContext.Guests.Find(Guestid);

            if (CheckGuest != null)
            {
                CheckGuest.Guest_Name = updateGuestWithoutId.Guest_Name;
                CheckGuest.Guest_Phone_Number = updateGuestWithoutId.Guest_Phone_Number;
                CheckGuest.Guest_Age = updateGuestWithoutId.Guest_Age;
                CheckGuest.Guest_Email = updateGuestWithoutId.Guest_Email;
                CheckGuest.Guest_Address = updateGuestWithoutId.Guest_Address;
                CheckGuest.Guest_Aadhar_Id = updateGuestWithoutId.Guest_Aadhar_Id;

                dbContext.SaveChanges();

                return Ok("Update Successfull");
            }
            else
            {
                return NotFound("No guest found");
            }

            
        }
        #endregion
    }
}
