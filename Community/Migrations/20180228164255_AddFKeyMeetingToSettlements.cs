using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Community.Migrations
{
    public partial class AddFKeyMeetingToSettlements : Migration
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
                name: "FK_Meetings_Settlements_CityId",
                table: "Meetings",
                column: "CityId",
                principalTable: "Settlements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Settlements_CityId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_CityId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Meetings");

            migrationBuilder.AddColumn<int>(
                name: "MeetingId",
                table: "Settlements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_MeetingId",
                table: "Settlements",
                column: "MeetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Settlements_Meetings_MeetingId",
                table: "Settlements",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
