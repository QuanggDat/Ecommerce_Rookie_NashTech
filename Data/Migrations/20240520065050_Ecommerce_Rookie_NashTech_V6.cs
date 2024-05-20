using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Ecommerce_Rookie_NashTech_V6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "receiverAddress",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "receiverFullname",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "receiverPhonenumber",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "receiverAddress",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "receiverFullname",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "receiverPhonenumber",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
