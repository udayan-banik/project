using HotelManagementSystem.Data;
using HotelManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Tree;
using System.Security.Cryptography.X509Certificates;

namespace HotelManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillController : Controller
    {
        private readonly HMSApiDbcontext dbContext;

        public BillController(HMSApiDbcontext dbContext)
        {
            this.dbContext = dbContext;
        }

        #region GetBill
        [HttpGet]
        [Route("/GetBill")]
        public IEnumerable<Bill> GetBill()
        {
            return dbContext.Bills;
        }
        #endregion

        #region AddBill
        [HttpPost]
        [Route("/AddBill")]
        public IActionResult AddBill(int CheckGuestId,int CheckReservationId)
        {
            var CheckGuest = dbContext.Guests.Find(CheckGuestId);
            var CheckReservation = dbContext.Room_Reservations.Find(CheckReservationId);

            if (CheckGuest == null)
                return NotFound("This Guest Id is not present");

            if (CheckReservation == null)
                return NotFound("This reservation Id is not present");

            float CalculateAmount()
            {
                var CheckIn  = CheckReservation.Resevation_Check_In;
                var CheckOut = CheckReservation.Resevation_Check_Out;

                int No_of_days = (CheckOut - CheckIn).Days;


                int Roomid = CheckReservation.Room_Id;
                
                var room = dbContext.Rooms.Find(Roomid);

                float AmountPerDay = room.Room_Price ;

                return (AmountPerDay) * (No_of_days);
            }

            var Bill = new Bill();
            {

                Bill.Bill_Amount = CalculateAmount();

                Bill.Bill_Date = DateTime.Now;

                Bill.Guest_Id = CheckGuest.Guest_Id;

                Bill.Reservation_Id = CheckReservation.Reservation_Id;

            }
            dbContext.Bills.Add(Bill);
            dbContext.SaveChanges();

            return Ok("New Bill Added");
        }
        #endregion

        #region UpdateBill
        [HttpPut]
        [Route("/UpdateBill")]
        public IActionResult UpdateBill(int Billid, AddUpdateBillWithoutId updateBillWithoutId, int CheckGuestId, int CheckReservationId)
        {
            var result = dbContext.Bills.Find(Billid);

            var CheckGuest = dbContext.Guests.Find(CheckGuestId);
            var CheckReservation = dbContext.Room_Reservations.Find(CheckReservationId);



            if (result != null)
            {
                
                if (CheckGuest == null)
                {
                    return NotFound("This Guest Id is not present");
                }
                else
                {
                    result.Guest_Id = CheckGuest.Guest_Id;
                }

                
                if (CheckReservation == null)
                {
                    return NotFound("This reservation Id is not present");
                }
                else
                {
                    result.Reservation_Id = CheckReservation.Reservation_Id;
                }
                result.Bill_Amount = updateBillWithoutId.Bill_Amount;
                result.Bill_Date = DateTime.Now; ;

                dbContext.SaveChanges();

                return Ok("Update Successfull");
            }
            else
            {
                return NotFound("No bill id found");
            }
        }
        #endregion

        #region DeleteBill
        [HttpDelete]
        [Route("/DeleteBill")]
        public IActionResult DeleteBill(int Billid)
        {
            var result = dbContext.Bills.Find(Billid);

            if (result != null)
            {
                dbContext.Bills.Remove(result);
                dbContext.SaveChanges();
                return Ok("Bill Deleted");
            }
            return NotFound();
        }
        #endregion
    }
}
