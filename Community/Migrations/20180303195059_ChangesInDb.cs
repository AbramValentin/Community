using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Community.Migrations
{
    public partial class ChangesInDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Settlements_SettlementsId",
                table: "Meetings");

            migrationBuilder.DropTable(
                name: "Settlements");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_SettlementsId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "SettlementsId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "UsersMax",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "UsersMin",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Areas");

            migrationBuilder.AddColumn<int>(
                name: "CitiesId",
                table: "Meetings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Meetings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndTime",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Areas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

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

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_CityId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CitiesId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Areas");

            migrationBuilder.AddColumn<int>(
                name: "SettlementsId",
                table: "Meetings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsersMax",
                table: "Meetings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsersMin",
                table: "Meetings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Regions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "Areas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AreaId = table.Column<int>(nullable: false),
                    City = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlements", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_SettlementsId",
                table: "Meetings",
                column: "SettlementsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Settlements_SettlementsId",
                table: "Meetings",
                column: "SettlementsId",
                principalTable: "Settlements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
