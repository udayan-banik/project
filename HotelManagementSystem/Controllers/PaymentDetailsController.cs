using HotelManagementSystem.Data;
using HotelManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentDetailsController : Controller
    {
        private readonly HMSApiDbcontext dbContext;
        public PaymentDetailsController(HMSApiDbcontext dbContext)
        {
            this.dbContext = dbContext;
        }
        #region GetPaymentDetails
        [HttpGet]
        [Route("/GetPaymentDetails")]
        public IActionResult GetPaymentDetails()
        {
            return Ok(dbContext.Payment_Details.ToList());
        }
        #endregion

        #region AddPaymentDetails
        [HttpPost]
        [Route("/AddPaymentDetails")]
        public IActionResult AddPaymentDetails(AddUpdatePaymentDetailsWithoutId addPaymentDetailsWithoutId,int CheckBillId)
        {
            var CheckBill = dbContext.Bills.Find(CheckBillId);
            var Payment = new Payment_Detail();

            Payment.Payment_Card = addPaymentDetailsWithoutId.Payment_Card;
            Payment.Payment_Card_Holder_Name = addPaymentDetailsWithoutId.Payment_Card_Holder_Name;
            if(CheckBill == null)
            {
                return NotFound("This Bill Id is not present");
            }
            else
            {
                Payment.Bill_Id = CheckBill.Bill_Id;
            }
            
            dbContext.Payment_Details.Add(Payment);
            dbContext.SaveChanges();

            return Ok("New Payment Details Added");
        }
        #endregion

        #region UpdatePaymentDetails
        [HttpPut]
        [Route("/UpdatePaymentDetails")]
        public IActionResult UpdatePaymentDetails(int PaymentId, AddUpdatePaymentDetailsWithoutId updatePaymentDetailsWithoutId, int CheckBillId)
        {
            var result = dbContext.Payment_Details.Find(PaymentId);
            var CheckBill = dbContext.Bills.Find(CheckBillId);

            if (result != null)
            {
                result.Payment_Card = updatePaymentDetailsWithoutId.Payment_Card;
                result.Payment_Card_Holder_Name = updatePaymentDetailsWithoutId.Payment_Card_Holder_Name;
                
                if (CheckBill == null)
                {
                    return NotFound("This Bill Id is not present");
                }
                else
                {
                    result.Bill_Id = CheckBill.Bill_Id;
                }

                dbContext.SaveChanges();

                return Ok("Update Successfull");
            }

            return NotFound();
        }
        #endregion
    }
}
