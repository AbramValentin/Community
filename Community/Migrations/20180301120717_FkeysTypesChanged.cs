using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Community.Migrations
{
    public partial class FkeysTypesChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Settlements_CityId",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_MeetingCategories_MeetingCategoryId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_CityId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Meetings");

            migrationBuilder.AlterColumn<int>(
                name: "MeetingCategoryId",
                table: "Meetings",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SettlementsId",
                table: "Meetings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_SettlementsId",
                table: "Meetings",
                column: "SettlementsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_MeetingCategories_MeetingCategoryId",
                table: "Meetings",
                column: "MeetingCategoryId",
                principalTable: "MeetingCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Settlements_SettlementsId",
                table: "Meetings",
                column: "SettlementsId",
                principalTable: "Settlements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_MeetingCategories_MeetingCategoryId",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Settlements_SettlementsId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_SettlementsId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "SettlementsId",
                table: "Meetings");

            migrationBuilder.AlterColumn<int>(
                name: "MeetingCategoryId",
                table: "Meetings",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Meetings",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_MeetingCategories_MeetingCategoryId",
                table: "Meetings",
                column: "MeetingCategoryId",
                principalTable: "MeetingCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
