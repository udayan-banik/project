using HotelManagementSystem.Data;
using HotelManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
        public class EmployeeController : Controller
        {
        private readonly HMSApiDbcontext dbContext;
        public EmployeeController(HMSApiDbcontext dbContext)
        {
            this.dbContext = dbContext;
        }
        #region Login
        [HttpPost]
        [Route("/LoginEmployee")]
        public IActionResult LoginEmployee(int EmployeeId, string EmployeePass)
        {
            Employee employee = new Employee();
            foreach (var item in dbContext.Employees)
            {
                if (item.Employee_Id == EmployeeId&& item.Employee_Password == EmployeePass)
                {
                    return Ok("Login of Employee is done");
                }
            }
            return NotFound("Wrong Credentials");
        }
        #endregion

        #region GetReceptionist
        [HttpGet]
        [Route("/GetReceptionist")]
        [Authorize]
        public IActionResult GetEmployee()
        {
            return Ok(dbContext.Employees.Where(x =>x.Employee_Designation == "Receptionist").ToList());
        }

        #endregion

        #region AddReceptionist

        [HttpPost]
        [Route("/AddReceptionist")]
        [Authorize]
        public IActionResult AddEmployee(AddUpdateEmployeeWithoutId addEmployeeWithoutId)
        {
            var Employee = new Employee()
            {
                Employee_Designation = "Receptionist",
                Employee_Name = addEmployeeWithoutId.Employee_Name,
                Employee_Password = addEmployeeWithoutId.Employee_Password,
                Employee_Salary = addEmployeeWithoutId.Employee_Salary,
                Employee_Email = addEmployeeWithoutId.Employee_Email,
                Employee_Age = addEmployeeWithoutId.Employee_Age,
                Employee_PhoneNo = addEmployeeWithoutId.Employee_PhoneNo,
                Employee_Address = addEmployeeWithoutId.Employee_Address,
            };
            dbContext.Employees.Add(Employee);
            dbContext.SaveChanges();

            return Ok("New Employee Added");
        }
        #endregion

        #region UpdateReceptionist

        [HttpPut]
        [Route("/UpdateReceptionist")]
        [Authorize]
        public IActionResult UpdateEmployee( int Employeeid,AddUpdateEmployeeWithoutId updateEmployeeWithoutId)
        {
            var CheckEmployee = dbContext.Employees.Find(Employeeid);

            if(CheckEmployee != null)
            {
                if(CheckEmployee.Employee_Designation == "Receptionist")
                {
                    CheckEmployee.Employee_Email = updateEmployeeWithoutId.Employee_Email;
                    CheckEmployee.Employee_PhoneNo = updateEmployeeWithoutId.Employee_PhoneNo;
                    CheckEmployee.Employee_Designation = "Receptionist";
                    CheckEmployee.Employee_Name = updateEmployeeWithoutId.Employee_Name;
                    CheckEmployee.Employee_Address = updateEmployeeWithoutId.Employee_Address;
                    CheckEmployee.Employee_Age = updateEmployeeWithoutId.Employee_Age;
                    CheckEmployee.Employee_Password = updateEmployeeWithoutId.Employee_Password;
                    CheckEmployee.Employee_Salary = updateEmployeeWithoutId.Employee_Salary;

                    dbContext.SaveChanges();

                    return Ok("Update Successfull");
                }

                return NotFound("You are not allowed");
            }
            else
            {
                return NotFound("No such employee not found");
            }

            
        }

        #endregion

        #region DeleteReceptionist
        [HttpDelete]
        [Route("/DeleteReceptionist")]
        [Authorize]
        public IActionResult DeleteEmployee(int Employeeid)
        {
            var CheckEmployee = dbContext.Employees.Find(Employeeid);
            
            if(CheckEmployee != null)
            {
                if(CheckEmployee.Employee_Designation == "Receptionist")
                {
                    dbContext.Employees.Remove(CheckEmployee);
                    dbContext.SaveChanges();
                    return Ok("Employee Deleted");

                }
                else
                {
                    return NotFound("You are not allowed");
                }
               
            }
            return NotFound("No such employee not found");
        }
        #endregion
    }
}
