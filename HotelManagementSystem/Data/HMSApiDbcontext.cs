using HotelManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Data
{
    public class HMSApiDbcontext : DbContext
    {
        public HMSApiDbcontext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Bill> Bills{ get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Payment_Detail> Payment_Details { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Room_Reservation> Room_Reservations{ get; set; }

    }
}
