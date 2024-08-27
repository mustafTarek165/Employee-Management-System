using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class addmirationRemoveNulls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sanctions_SanctionTypes_SanctionTypeId",
                table: "Sanctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacations_VacationTypes_VacationTypeId",
                table: "Vacations");

            migrationBuilder.AlterColumn<int>(
                name: "VacationTypeId",
                table: "Vacations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SanctionTypeId",
                table: "Sanctions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sanctions_SanctionTypes_SanctionTypeId",
                table: "Sanctions",
                column: "SanctionTypeId",
                principalTable: "SanctionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_VacationTypes_VacationTypeId",
                table: "Vacations",
                column: "VacationTypeId",
                principalTable: "VacationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sanctions_SanctionTypes_SanctionTypeId",
                table: "Sanctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacations_VacationTypes_VacationTypeId",
                table: "Vacations");

            migrationBuilder.AlterColumn<int>(
                name: "VacationTypeId",
                table: "Vacations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SanctionTypeId",
                table: "Sanctions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Sanctions_SanctionTypes_SanctionTypeId",
                table: "Sanctions",
                column: "SanctionTypeId",
                principalTable: "SanctionTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_VacationTypes_VacationTypeId",
                table: "Vacations",
                column: "VacationTypeId",
                principalTable: "VacationTypes",
                principalColumn: "Id");
        }
    }
}
