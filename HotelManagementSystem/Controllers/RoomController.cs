using HotelManagementSystem.Data;
using HotelManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace HotelManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : Controller
    {
        private readonly HMSApiDbcontext dbContext;
        public RoomController(HMSApiDbcontext dbContext)
        {
            this.dbContext = dbContext;
        }

        #region GetRooms
        [HttpGet]
        [Route("/GetRooms")]
        public IActionResult GetRoom()
        {
            return Ok(dbContext.Rooms.ToList());
        }
        #endregion

        #region BookedRooms
        [HttpGet]
        [Route("/BookedRooms")]
        public IActionResult GetBookedRoom()
        {
            return Ok(dbContext.Rooms.Where(x => x.Room_Status == false).ToList());
        }
        #endregion

        #region UnbookedRooms
        [HttpGet]
        [Route("/UnbookedRooms")]
        public IActionResult GetUnBookedRoom()
        {
            return Ok(dbContext.Rooms.Where(x => x.Room_Status == true).ToList());
        }
        #endregion

        #region AddRoom
        [HttpPost]
        [Route("/AddRoom")]
        public IActionResult AddRoom(AddUpdateRoomWithoutId addRoomWithoutId)
        {
            var room = new Room()
            {
                Room_Comment = addRoomWithoutId.Room_Comment,
                Room_Inventory = addRoomWithoutId.Room_Inventory,
                Room_Price = addRoomWithoutId.Room_Price,
                Room_Status = addRoomWithoutId.Room_Status,
            };
            dbContext.Rooms.Add(room);
            dbContext.SaveChanges();
            return Ok("{\"msg\":\"Room Added\"}");
        }
        #endregion

        #region UpdateRoom
        [HttpPut]
        [Route("/UpdateRoom")]
        public IActionResult UpdateRoom(int Roomid,AddUpdateRoomWithoutId updateRoomWithoutId)
        {
            var CheckRoom = dbContext.Rooms.Find(Roomid);

            if(CheckRoom != null)
            {
                CheckRoom.Room_Price = updateRoomWithoutId.Room_Price;
                CheckRoom.Room_Comment = updateRoomWithoutId.Room_Comment;
                CheckRoom.Room_Status = updateRoomWithoutId.Room_Status;
                CheckRoom.Room_Inventory = updateRoomWithoutId.Room_Inventory;

                dbContext.SaveChanges();

                return Ok("{\"msg\":\"Room Updated\"}");
            }

            return Ok("{\"msg\":\"Room Not Found\"}");
        }
        #endregion

        #region DeleteRoom
        //[HttpDelete]
        //[Route("/DeleteRoom/{RoomId:int}")]
        //public IActionResult DeleteRoom([FromRoute]int RoomId)
        //{
        //    var CheckRoom = dbContext.Rooms.Find(RoomId);

        //    if (CheckRoom != null)
        //    {
        //        dbContext.Rooms.Remove(CheckRoom);
        //        dbContext.SaveChanges();

        //        return Ok("{\"msg\":\"Room deleted\"}");
        //    }
        //    return Ok("{\"msg\":\"Room not found\"}");
        //}

        [HttpDelete]
        [Route("/DeleteRoom")]
        public IActionResult DeleteRoom(int RoomId)
        {
            var CheckRoom = dbContext.Rooms.Find(RoomId);

            if (CheckRoom != null)
            {
                dbContext.Rooms.Remove(CheckRoom);
                dbContext.SaveChanges();

                return Ok("{\"msg\":\"Room deleted\"}");
            }
            return Ok("{\"msg\":\"Room not found\"}");
        }
        #endregion
    }
}
