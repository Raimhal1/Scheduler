using Microsoft.EntityFrameworkCore.Migrations;

namespace MySchedulerWeb.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "DayEvent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "DayEvent",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
