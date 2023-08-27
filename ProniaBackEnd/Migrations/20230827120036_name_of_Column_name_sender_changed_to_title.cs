using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProniaBackEnd.Migrations
{
    public partial class name_of_Column_name_sender_changed_to_title : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sender",
                table: "EmailMessage",
                newName: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "EmailMessage",
                newName: "Sender");
        }
    }
}
