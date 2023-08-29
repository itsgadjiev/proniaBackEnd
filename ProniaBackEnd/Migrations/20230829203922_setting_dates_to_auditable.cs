using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProniaBackEnd.Migrations
{
    public partial class setting_dates_to_auditable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE ""Sliders""
                SET ""CreatedOn"" = now(),
	                ""UpdatedOn"" = now()");

            migrationBuilder.Sql(@"UPDATE ""Products""
                SET ""CreatedOn"" = now(),
	                ""UpdatedOn"" = now()");

            migrationBuilder.Sql(@"UPDATE ""Categories""
                SET ""CreatedOn"" = now(),
	                ""UpdatedOn"" = now()");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
