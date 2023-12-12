using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheckRent.Migrations
{
    public partial class IntialCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rents",
                columns: table => new
                {
                    Rent_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenant_fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    End_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Faculty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wrote_date = table.Column<string>(type: "nvarchar(max)", nullable: true)
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rents");
        }
    }
}
