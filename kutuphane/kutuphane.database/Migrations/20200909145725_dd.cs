using Microsoft.EntityFrameworkCore.Migrations;

namespace kutuphane.database.Migrations
{
    public partial class dd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Writers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Writers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
