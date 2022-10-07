using HotelManagementSystem.Data;
using HotelManagementSystem.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace HotelManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private readonly HMSApiDbcontext dbContext;
        public ReservationController(HMSApiDbcontext dbContext)
        {
            this.dbContext = dbContext;
        }

        #region GetReservations
        [HttpGet]
        [Route("/GetReservations")]
        public IEnumerable<Room_Reservation> GetReservations()
        {
            return dbContext.Room_Reservations;
        }

        #endregion

        #region AddReservation
        [HttpPost]
        [Route("/AddReservation")]
        public IActionResult AddReservation(AddUpdateReservationWithoutId addReservationWithoutId,int CheckRoomId,int CheckGuestId)
        {
            var CheckRoom = dbContext.Rooms.Find(CheckRoomId);
            var CheckGuest = dbContext.Guests.Find(CheckGuestId);
            

            if (CheckGuest == null)
            {
                return NotFound("This Guest Id is not present");
            }
            if (CheckRoom == null)
            {
                return NotFound("This Room Id is not present");
            }
            else if (CheckRoom.Room_Status == false)
            {
                return NotFound("This Room is already booked");
            }

            var room_Reservation = new Room_Reservation();
            {
                room_Reservation.Resevation_Check_In = addReservationWithoutId.Resevation_Check_In;
                room_Reservation.Resevation_Check_Out = addReservationWithoutId.Resevation_Check_Out;
                room_Reservation.Reservation_No_of_Guests = addReservationWithoutId.Reservation_No_of_Guests;
                room_Reservation.Reservation_Status = true;
                room_Reservation.Room = CheckRoom;
                room_Reservation.Guest = CheckGuest;
            }

            dbContext.Room_Reservations.Add(room_Reservation);

            CheckRoom.Room_Status = false;

            dbContext.SaveChanges();

            #region Email to Guest
            var GuestEmail = CheckGuest.Guest_Email;

            var email = new MimeMessage();
            //email.From.Add(MailboxAddress.Parse("juwan.fay46@ethereal.email"));
            email.From.Add(MailboxAddress.Parse("udaysenpai@gmail.com"));
            email.To.Add(MailboxAddress.Parse(GuestEmail));
            email.Subject = "Dear " + CheckGuest.Guest_Name + " your reservation is confirmed at HMS Hotel"; //booking with 
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = "Your booking is confirmed "};

            using var smpt = new SmtpClient();
            //smpt.Connect("smtp.ethereal.email", 587,MailKit.Security.SecureSocketOptions.StartTls);
            smpt.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            //smpt.Authenticate("juwan.fay46@ethereal.email", "B7MarFqhRvZuJqmjTr");
            smpt.Authenticate("udaysenpai@gmail.com", "edhdyctllcfinmek");
            smpt.Send(email);
            smpt.Disconnect(true);
            #endregion

            return Ok("New Reservation Added");
        }
        #endregion

        #region UpdateReservation
        [HttpPut]
        [Route("/UpdateReservation")]
        public IActionResult UpdateReservation(int Reservationid, AddUpdateReservationWithoutId updateReservationWithoutId, int CheckRoomId, int CheckGuestId)
        {
            var result = dbContext.Room_Reservations.Find(Reservationid);
            var CheckRoom = dbContext.Rooms.Find(CheckRoomId);
            var CheckGuest = dbContext.Guests.Find(CheckGuestId);

            if (result != null)
            {
                result.Resevation_Check_In = updateReservationWithoutId.Resevation_Check_In;
                result.Resevation_Check_Out = updateReservationWithoutId.Resevation_Check_Out;
                result.Reservation_No_of_Guests = updateReservationWithoutId.Reservation_No_of_Guests;


                if (CheckGuest == null)
                {
                    return NotFound("This Guest Id is not present");
                }
                else
                {
                    result.Guest_Id= CheckGuest.Guest_Id;
                }
      

                if (CheckRoom == null)
                {
                    return NotFound("This Room Id is not present");
                }
                else
                {
                    result.Room_Id = CheckRoom.Room_Id;
                }

                dbContext.SaveChanges();

                return Ok("Update Successfull");
            }

            return NotFound();
        }
        #endregion

        #region DeleteReservation
        [HttpDelete]
        [Route("/DeleteReservation")]
        public IActionResult DeleteReservation( int Reservationid)
        {
            var CheckReservation = dbContext.Room_Reservations.Find(Reservationid);

            if (CheckReservation != null)
            {
                dbContext.Room_Reservations.Remove(CheckReservation);
                dbContext.SaveChanges();
                return Ok("Reservation Deleted");
            }
            return NotFound("This reservation not found");
        }
        #endregion

        #region CancelReservation
        [HttpPost]
        [Route("/CancelReservation")]
        public IActionResult CancelReservation(int CheckReservationId)
        {
            
            var CheckReservation = dbContext.Room_Reservations.Find(CheckReservationId);

            if (CheckReservation == null)
            {
                return NotFound("This Reservation Id is not present");
            }

            var CheckGuest = CheckReservation.Guest_Id;

            var CheckRoom = CheckReservation.Room_Id;

            CheckReservation.Reservation_Status = false;

            var room = dbContext.Rooms.Find(CheckRoom);
            var guest = dbContext.Guests.Find(CheckGuest);

            room.Room_Status = true;

            dbContext.SaveChanges();

            #region Email to Guest
            var GuestEmail = guest.Guest_Email;
            //var GuestEmail = "banikuday2708@gmail.com";

            var email = new MimeMessage();
            //email.From.Add(MailboxAddress.Parse("juwan.fay46@ethereal.email"));
            email.From.Add(MailboxAddress.Parse("udaysenpai@gmail.com"));
            email.To.Add(MailboxAddress.Parse(GuestEmail));
            email.Subject = "Dear " + guest.Guest_Name + " your reservation is cancelled"; //booking with 
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = "Your booking is cancelled" };

            using var smpt = new SmtpClient();
            //smpt.Connect("smtp.ethereal.email", 587,MailKit.Security.SecureSocketOptions.StartTls);
            smpt.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            //smpt.Authenticate("juwan.fay46@ethereal.email", "B7MarFqhRvZuJqmjTr");
            smpt.Authenticate("udaysenpai@gmail.com", "edhdyctllcfinmek");
            smpt.Send(email);
            smpt.Disconnect(true);
            #endregion

            return Ok("Reservation Cancelled");
        }
        #endregion

    }
}
