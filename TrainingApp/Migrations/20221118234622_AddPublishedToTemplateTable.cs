using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingApp.Migrations
{
    public partial class AddPublishedToTemplateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GradingSchema",
                table: "Templates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Templates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AssessmentGrades",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "AssessmentGrades",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "AssessmentGrades",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AssessmentGrades");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "AssessmentGrades");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "AssessmentGrades");

            migrationBuilder.AlterColumn<int>(
                name: "GradingSchema",
                table: "Templates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
