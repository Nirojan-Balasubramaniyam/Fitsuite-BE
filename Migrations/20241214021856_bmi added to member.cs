using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GYMFeeManagement_System_BE.Migrations
{
    /// <inheritdoc />
    public partial class bmiaddedtomember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Bmi",
                table: "Members",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bmi",
                table: "Members");
        }
    }
}
