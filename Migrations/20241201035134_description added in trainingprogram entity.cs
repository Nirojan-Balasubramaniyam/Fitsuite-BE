using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GYMFeeManagement_System_BE.Migrations
{
    /// <inheritdoc />
    public partial class descriptionaddedintrainingprogramentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TrainingPrograms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TrainingPrograms");
        }
    }
}
