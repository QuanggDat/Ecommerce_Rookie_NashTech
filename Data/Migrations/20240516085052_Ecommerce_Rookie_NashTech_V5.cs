using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Ecommerce_Rookie_NashTech_V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "OrderDetail");

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "OrderDetail",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "OrderDetail");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
