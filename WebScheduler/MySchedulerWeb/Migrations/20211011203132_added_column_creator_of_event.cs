using Microsoft.EntityFrameworkCore.Migrations;

namespace MySchedulerWeb.Migrations
{
    public partial class added_column_creator_of_event : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "DayEvents",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creator",
                table: "DayEvents");
        }
    }
}
