using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GYMFeeManagement_System_BE.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "WorkoutPlans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "WorkoutPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "WorkoutPlans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDone",
                table: "WorkoutPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlans_MemberId",
                table: "WorkoutPlans",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPlans_Members_MemberId",
                table: "WorkoutPlans",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlans_Members_MemberId",
                table: "WorkoutPlans");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutPlans_MemberId",
                table: "WorkoutPlans");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "WorkoutPlans");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "WorkoutPlans");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "WorkoutPlans");

            migrationBuilder.DropColumn(
                name: "isDone",
                table: "WorkoutPlans");
        }
    }
}
