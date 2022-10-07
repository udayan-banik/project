using HotelManagementSystem.Data;
using HotelManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HotelManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : Controller
    {
        private readonly HMSApiDbcontext dbContext;

        public OwnerController(HMSApiDbcontext dbContext)
        {
            this.dbContext = dbContext;
        }

        #region LoginOwner
        [HttpPost]
        [Route("/LoginOwner")]
        public IActionResult LoginOwner(int OwnerId, string OwnerPass)
        {
            Owner owner = new Owner();
            foreach (var item in dbContext.Owners)
            {
                if (item.Owner_Id == OwnerId && item.Owner_Password == OwnerPass)
                {
                    return Ok("Login of Owner is done");
                }
            }
            return NotFound("Wrong Credentials");
        }
        #endregion

        #region AllDepartments
        [HttpGet]
        [Route("/Departments")]
        [Authorize]
        public IActionResult GetEmployee()
        {
            return Ok(dbContext.Employees.ToList());
        }
        #endregion

        #region AddEmployee
        [HttpPost]
        [Route("/AddEmployee")]
        [Authorize]
        public IActionResult AddEmployee(AddUpdateEmployeeWithoutId addEmployeeWithoutId)
        {
            var Employee = new Employee()
            {
                Employee_Designation = addEmployeeWithoutId.Employee_Designation,
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

        #region UpdateEmployee
        [HttpPut]
        [Route("/UpdateEmployee ")]
        [Authorize]
        public IActionResult UpdateEmployee(int Employeeid, AddUpdateEmployeeWithoutId updateEmployeeWithoutId)
        {
            var CheckEmployee = dbContext.Employees.Find(Employeeid);

            if (CheckEmployee != null)
            {
                CheckEmployee.Employee_Email = updateEmployeeWithoutId.Employee_Email;
                CheckEmployee.Employee_PhoneNo = updateEmployeeWithoutId.Employee_PhoneNo;
                CheckEmployee.Employee_Designation = updateEmployeeWithoutId.Employee_Designation;
                CheckEmployee.Employee_Name = updateEmployeeWithoutId.Employee_Name;
                CheckEmployee.Employee_Address = updateEmployeeWithoutId.Employee_Address;
                CheckEmployee.Employee_Age = updateEmployeeWithoutId.Employee_Age;
                CheckEmployee.Employee_Password = updateEmployeeWithoutId.Employee_Password;
                CheckEmployee.Employee_Salary = updateEmployeeWithoutId.Employee_Salary;

                dbContext.SaveChanges();

                return Ok("Update Successfull");
            }

            return NotFound("Employee not found");
        }
        #endregion

        #region DeleteEmployee
        [HttpDelete]
        [Route("/DeleteEmployee")]
        [Authorize]
        public IActionResult DeleteEmployee( int Employeeid)
        {
            var CheckEmployee = dbContext.Employees.Find(Employeeid);

            if (CheckEmployee != null)
            {
                dbContext.Employees.Remove(CheckEmployee);
                dbContext.SaveChanges();
                return Ok("Employee Deleted");
            }
            return NotFound("Employee not found");
        }
        #endregion

        #region AllRooms
        [HttpGet]
        [Route("/AllRooms")]
        [Authorize]
        public IActionResult GetRoom()
        {
            return Ok(dbContext.Rooms.ToList());
        }
        #endregion

        #region AllGuests
        [HttpGet]
        [Route("/AllGuests")]
        [Authorize]
        public IActionResult GetGuest()
        {
            return Ok(dbContext.Guests.ToList());
        }
        #endregion
    }
}
