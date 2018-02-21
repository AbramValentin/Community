using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Community.Migrations
{
    public partial class CorrectionOfFieldsNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_AspNetUsers_CreatorIdId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_CreatorIdId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Meetings");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Meetings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Meetings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MeetingDate",
                table: "Meetings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_CreatorId",
                table: "Meetings",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_AspNetUsers_CreatorId",
                table: "Meetings",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_AspNetUsers_CreatorId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_CreatorId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "MeetingDate",
                table: "Meetings");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Meetings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorIdId",
                table: "Meetings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "Meetings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_CreatorIdId",
                table: "Meetings",
                column: "CreatorIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_AspNetUsers_CreatorIdId",
                table: "Meetings",
                column: "CreatorIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
