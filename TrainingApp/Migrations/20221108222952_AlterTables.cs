using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingApp.Migrations
{
    public partial class AlterTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentGradeElements");

            migrationBuilder.DropTable(
                name: "GradeElements");

            migrationBuilder.AlterColumn<int>(
                name: "GradingSchema",
                table: "Templates",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Grades",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "Grades",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Grades",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AssessmentTasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassGrade",
                table: "Assessments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AssessmentTasks");

            migrationBuilder.DropColumn(
                name: "PassGrade",
                table: "Assessments");

            migrationBuilder.AlterColumn<string>(
                name: "GradingSchema",
                table: "Templates",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "AssessmentGradeElements",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssessmentGradeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFail = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentGradeElements", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AssessmentGradeElements_AssessmentGrades_AssessmentGradeID",
                        column: x => x.AssessmentGradeID,
                        principalTable: "AssessmentGrades",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GradeElements",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFail = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeElements", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GradeElements_Grades_GradeID",
                        column: x => x.GradeID,
                        principalTable: "Grades",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentGradeElements_AssessmentGradeID",
                table: "AssessmentGradeElements",
                column: "AssessmentGradeID");

            migrationBuilder.CreateIndex(
                name: "IX_GradeElements_GradeID",
                table: "GradeElements",
                column: "GradeID");
        }
    }
}
