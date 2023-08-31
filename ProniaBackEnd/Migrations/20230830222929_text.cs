using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProniaBackEnd.Migrations
{
    public partial class text : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "BasketItems");

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "BasketItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "BasketItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_ColorId",
                table: "BasketItems",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_SizeId",
                table: "BasketItems",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Color_ColorId",
                table: "BasketItems",
                column: "ColorId",
                principalTable: "Color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Size_SizeId",
                table: "BasketItems",
                column: "SizeId",
                principalTable: "Size",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Color_ColorId",
                table: "BasketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Size_SizeId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_ColorId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_SizeId",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "BasketItems");

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "BasketItems",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
