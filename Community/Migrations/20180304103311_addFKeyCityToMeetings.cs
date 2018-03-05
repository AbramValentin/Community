using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Community.Migrations
{
    public partial class addFKeyCityToMeetings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Meetings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_CityId",
                table: "Meetings",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Cities_CityId",
                table: "Meetings",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Cities_CityId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_CityId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Meetings");
        }
    }
}
