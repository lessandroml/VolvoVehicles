using Microsoft.EntityFrameworkCore.Migrations;

namespace VolvoTrucks.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Truck",
                columns: table => new
                {
                    TruckID = table.Column<int>(nullable: false),
                    ModelYear = table.Column<int>(nullable: false),
                    ManufacturingYear = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Truck", x => x.TruckID);
                });

            migrationBuilder.CreateTable(
                name: "TruckModel",
                columns: table => new
                {
                    TruckID = table.Column<int>(nullable: false),
                    ModelName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckModel", x => x.TruckID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Truck");

            migrationBuilder.DropTable(
                name: "TruckModel");
        }
    }
}
