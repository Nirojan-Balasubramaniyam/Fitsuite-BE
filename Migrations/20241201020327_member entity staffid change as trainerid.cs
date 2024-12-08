using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GYMFeeManagement_System_BE.Migrations
{
    /// <inheritdoc />
    public partial class memberentitystaffidchangeastrainerid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Staffs_StaffId",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "Members",
                newName: "TrainerId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_StaffId",
                table: "Members",
                newName: "IX_Members_TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Staffs_TrainerId",
                table: "Members",
                column: "TrainerId",
                principalTable: "Staffs",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Staffs_TrainerId",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "TrainerId",
                table: "Members",
                newName: "StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_TrainerId",
                table: "Members",
                newName: "IX_Members_StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Staffs_StaffId",
                table: "Members",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
