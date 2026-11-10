using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonationHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedByUserId",
                table: "Campaigns",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "GoalAmount",
                table: "Campaigns",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "GoalCurrency",
                table: "Campaigns",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_CreatedByUserId",
                table: "Campaigns",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_AspNetUsers_CreatedByUserId",
                table: "Campaigns",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_AspNetUsers_DonorUserId",
                table: "Donations",
                column: "DonorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_AspNetUsers_CreatedByUserId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Donations_AspNetUsers_DonorUserId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_CreatedByUserId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "GoalAmount",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "GoalCurrency",
                table: "Campaigns");

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "Campaigns",
                type: "rowversion",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "rowversion",
                oldRowVersion: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByUserId",
                table: "Campaigns",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
