using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class OnCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branchs_Departments_DepartmentId",
                table: "Branchs");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_GeneralDepartments_GeneralDepartmentId",
                table: "Departments");

            migrationBuilder.AddForeignKey(
                name: "FK_Branchs_Departments_DepartmentId",
                table: "Branchs",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_GeneralDepartments_GeneralDepartmentId",
                table: "Departments",
                column: "GeneralDepartmentId",
                principalTable: "GeneralDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branchs_Departments_DepartmentId",
                table: "Branchs");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_GeneralDepartments_GeneralDepartmentId",
                table: "Departments");

            migrationBuilder.AddForeignKey(
                name: "FK_Branchs_Departments_DepartmentId",
                table: "Branchs",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_GeneralDepartments_GeneralDepartmentId",
                table: "Departments",
                column: "GeneralDepartmentId",
                principalTable: "GeneralDepartments",
                principalColumn: "Id");
        }
    }
}
