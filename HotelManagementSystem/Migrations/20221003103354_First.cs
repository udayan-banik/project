using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementSystem.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Employee_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employee_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Employee_Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Employee_Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Employee_Salary = table.Column<float>(type: "real", nullable: false),
                    Employee_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Employee_Age = table.Column<int>(type: "int", nullable: false),
                    Employee_PhoneNo = table.Column<long>(type: "bigint", nullable: false),
                    Employee_Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Employee_Id);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Guest_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guest_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Guest_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Guest_Age = table.Column<int>(type: "int", nullable: false),
                    Guest_Phone_Number = table.Column<long>(type: "bigint", nullable: false),
                    Guest_Aadhar_Id = table.Column<long>(type: "bigint", nullable: false),
                    Guest_Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Guest_Id);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Owner_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Owner_Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Owner_Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Room_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Room_Status = table.Column<bool>(type: "bit", nullable: false),
                    Room_Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Room_Inventory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Room_Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Room_Id);
                });

            migrationBuilder.CreateTable(
                name: "Room_Reservations",
                columns: table => new
                {
                    Reservation_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Resevation_Check_In = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Resevation_Check_Out = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reservation_No_of_Guests = table.Column<int>(type: "int", nullable: false),
                    Guest_Id = table.Column<int>(type: "int", nullable: false),
                    Room_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room_Reservations", x => x.Reservation_Id);
                    table.ForeignKey(
                        name: "FK_Room_Reservations_Guests_Guest_Id",
                        column: x => x.Guest_Id,
                        principalTable: "Guests",
                        principalColumn: "Guest_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Room_Reservations_Rooms_Room_Id",
                        column: x => x.Room_Id,
                        principalTable: "Rooms",
                        principalColumn: "Room_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Bill_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bill_Amount = table.Column<float>(type: "real", nullable: false),
                    Bill_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reservation_Id = table.Column<int>(type: "int", nullable: false),
                    Guest_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Bill_Id);
                    table.ForeignKey(
                        name: "FK_Bills_Room_Reservations_Reservation_Id",
                        column: x => x.Reservation_Id,
                        principalTable: "Room_Reservations",
                        principalColumn: "Reservation_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment_Details",
                columns: table => new
                {
                    Payment_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Payment_Card = table.Column<long>(type: "bigint", nullable: false),
                    Payment_Card_Holder_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bill_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment_Details", x => x.Payment_Id);
                    table.ForeignKey(
                        name: "FK_Payment_Details_Bills_Bill_Id",
                        column: x => x.Bill_Id,
                        principalTable: "Bills",
                        principalColumn: "Bill_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_Reservation_Id",
                table: "Bills",
                column: "Reservation_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Details_Bill_Id",
                table: "Payment_Details",
                column: "Bill_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Room_Reservations_Guest_Id",
                table: "Room_Reservations",
                column: "Guest_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Room_Reservations_Room_Id",
                table: "Room_Reservations",
                column: "Room_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Payment_Details");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Room_Reservations");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
