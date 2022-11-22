using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingApp.Migrations
{
    public partial class DefineTableAssociations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Trainee",
                table: "Assessments");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_TraineeID",
                table: "Assessments",
                column: "TraineeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Users_TraineeID",
                table: "Assessments",
                column: "TraineeID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_Users_TraineeID",
                table: "Assessments");

            migrationBuilder.DropIndex(
                name: "IX_Assessments_TraineeID",
                table: "Assessments");

            migrationBuilder.AddColumn<Guid>(
                name: "Trainee",
                table: "Assessments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
