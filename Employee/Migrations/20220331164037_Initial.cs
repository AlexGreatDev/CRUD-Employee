using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmployeeFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmployeePhone = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    EmployeeZip = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    HireDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
