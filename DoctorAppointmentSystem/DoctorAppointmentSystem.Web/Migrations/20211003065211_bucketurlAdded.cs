using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorAppointmentSystem.Web.Migrations
{
    public partial class bucketurlAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BucketUrl",
                table: "Doctors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BucketUrl",
                table: "Doctors");
        }
    }
}
