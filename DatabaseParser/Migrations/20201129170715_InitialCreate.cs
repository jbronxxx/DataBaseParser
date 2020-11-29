using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseParser.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayrollNumber = table.Column<string>(type: "varchar(10)", nullable: false),
                    ForeNames = table.Column<string>(type: "varchar(50)", nullable: false),
                    SurName = table.Column<string>(type: "varchar(50)", nullable: false),
                    DateOfBirth = table.Column<string>(type: "varchar(15)", nullable: true),
                    Telephone = table.Column<string>(type: "varchar(20)", nullable: true),
                    Mobile = table.Column<string>(type: "varchar(20)", nullable: true),
                    Adress = table.Column<string>(type: "varchar(50)", nullable: true),
                    Adress2 = table.Column<string>(type: "varchar(50)", nullable: true),
                    PostCode = table.Column<string>(type: "varchar(15)", nullable: true),
                    EmailHome = table.Column<string>(type: "varchar(50)", nullable: true),
                    StartDate = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
