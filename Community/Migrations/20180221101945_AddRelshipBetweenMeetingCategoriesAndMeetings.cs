using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Community.Migrations
{
    public partial class AddRelshipBetweenMeetingCategoriesAndMeetings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "MeetingCategoryId",
                table: "Meetings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_MeetingCategoryId",
                table: "Meetings",
                column: "MeetingCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_MeetingCategories_MeetingCategoryId",
                table: "Meetings",
                column: "MeetingCategoryId",
                principalTable: "MeetingCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_MeetingCategories_MeetingCategoryId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_MeetingCategoryId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "MeetingCategoryId",
                table: "Meetings");


            migrationBuilder.AddForeignKey(
                name: "FK_MeetingCategories_Meetings_MeetingId",
                table: "MeetingCategories",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
