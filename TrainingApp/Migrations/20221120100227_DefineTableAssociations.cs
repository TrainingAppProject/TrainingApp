using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingApp.Migrations
{
    public partial class DefineTableAssociations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Trainee",
                table: "Assessments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_CreatedID",
                table: "Assessments",
                column: "CreatedID");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_ExaminerID",
                table: "Assessments",
                column: "ExaminerID");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_TemplateID",
                table: "Assessments",
                column: "TemplateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Templates_TemplateID",
                table: "Assessments",
                column: "TemplateID",
                principalTable: "Templates",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Users_CreatedID",
                table: "Assessments",
                column: "CreatedID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Users_ExaminerID",
                table: "Assessments",
                column: "ExaminerID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_Templates_TemplateID",
                table: "Assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_Users_CreatedID",
                table: "Assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_Users_ExaminerID",
                table: "Assessments");

            migrationBuilder.DropIndex(
                name: "IX_Assessments_CreatedID",
                table: "Assessments");

            migrationBuilder.DropIndex(
                name: "IX_Assessments_ExaminerID",
                table: "Assessments");

            migrationBuilder.DropIndex(
                name: "IX_Assessments_TemplateID",
                table: "Assessments");

            migrationBuilder.DropColumn(
                name: "Trainee",
                table: "Assessments");
        }
    }
}
