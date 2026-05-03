using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Day2.Migrations
{
    /// <inheritdoc />
    public partial class rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Traines_TraineeId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Traines_Departments_DeptId",
                table: "Traines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Traines",
                table: "Traines");

            migrationBuilder.RenameTable(
                name: "Traines",
                newName: "Trainees");

            migrationBuilder.RenameIndex(
                name: "IX_Traines_DeptId",
                table: "Trainees",
                newName: "IX_Trainees_DeptId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainees",
                table: "Trainees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Trainees_TraineeId",
                table: "Results",
                column: "TraineeId",
                principalTable: "Trainees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_Departments_DeptId",
                table: "Trainees",
                column: "DeptId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Trainees_TraineeId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Departments_DeptId",
                table: "Trainees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainees",
                table: "Trainees");

            migrationBuilder.RenameTable(
                name: "Trainees",
                newName: "Traines");

            migrationBuilder.RenameIndex(
                name: "IX_Trainees_DeptId",
                table: "Traines",
                newName: "IX_Traines_DeptId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Traines",
                table: "Traines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Traines_TraineeId",
                table: "Results",
                column: "TraineeId",
                principalTable: "Traines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Traines_Departments_DeptId",
                table: "Traines",
                column: "DeptId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
