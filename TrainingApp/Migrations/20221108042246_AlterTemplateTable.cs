using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingApp.Migrations
{
    public partial class AlterTemplateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedID",
                table: "Templates",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AttemptsAllowedPerTask",
                table: "Templates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GradingSchema",
                table: "Templates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Templates_CreatedID",
                table: "Templates",
                column: "CreatedID");

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_Users_CreatedID",
                table: "Templates",
                column: "CreatedID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Templates_Users_CreatedID",
                table: "Templates");

            migrationBuilder.DropIndex(
                name: "IX_Templates_CreatedID",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "AttemptsAllowedPerTask",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "GradingSchema",
                table: "Templates");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedID",
                table: "Templates",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
