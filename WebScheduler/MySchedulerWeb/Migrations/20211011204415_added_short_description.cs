using Microsoft.EntityFrameworkCore.Migrations;

namespace MySchedulerWeb.Migrations
{
    public partial class added_short_description : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "DayEvents",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "DayEvents");
        }
    }
}
