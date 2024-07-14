using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movilissa_api.Migrations
{
    /// <inheritdoc />
    public partial class MakeStatusIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_BusStatus_StatusId",
                table: "Buses");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Buses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_BusStatus_StatusId",
                table: "Buses",
                column: "StatusId",
                principalTable: "BusStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_BusStatus_StatusId",
                table: "Buses");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Buses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_BusStatus_StatusId",
                table: "Buses",
                column: "StatusId",
                principalTable: "BusStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
