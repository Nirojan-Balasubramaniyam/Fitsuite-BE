using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GYMFeeManagement_System_BE.Migrations
{
    /// <inheritdoc />
    public partial class isactiveaddedformstaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Staffs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Staffs");
        }
    }
}
