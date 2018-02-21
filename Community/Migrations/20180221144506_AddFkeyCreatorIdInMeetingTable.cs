using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Community.Migrations
{
    public partial class AddFkeyCreatorIdInMeetingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Meetings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_CreatorIdId",
                table: "Meetings",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_AspNetUsers_CreatorIdId",
                table: "Meetings",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_AspNetUsers_CreatorIdId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_CreatorId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Meetings");
        }
    }
}
