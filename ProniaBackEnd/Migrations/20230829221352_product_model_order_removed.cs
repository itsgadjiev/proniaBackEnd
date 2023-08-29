using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProniaBackEnd.Migrations
{
    public partial class product_model_order_removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Order",
                table: "Products",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
