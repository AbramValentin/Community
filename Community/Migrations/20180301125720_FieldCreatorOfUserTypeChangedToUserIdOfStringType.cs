using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Community.Migrations
{
    public partial class FieldCreatorOfUserTypeChangedToUserIdOfStringType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Meetings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_UserId",
                table: "Meetings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_AspNetUsers_UserId",
                table: "Meetings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_AspNetUsers_UserId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_UserId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Meetings");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Meetings",
                nullable: true);

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
    }
}
