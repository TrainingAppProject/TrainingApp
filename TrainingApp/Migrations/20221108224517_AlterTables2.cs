using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingApp.Migrations
{
    public partial class AlterTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Templates_Grades_GradeID",
                table: "Templates");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Templates_GradeID",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "GradeID",
                table: "Templates");

            migrationBuilder.AlterColumn<int>(
                name: "GradingSchema",
                table: "Templates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "GradeID",
                table: "Templates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Templates_GradeID",
                table: "Templates",
                column: "GradeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_Grades_GradeID",
                table: "Templates",
                column: "GradeID",
                principalTable: "Grades",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
