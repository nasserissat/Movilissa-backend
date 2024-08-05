using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movilissa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BranchNotNullableOnBusType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "BusTypes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BusTypes_Brand_BrandId",
                table: "BusTypes",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "BusTypes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BusTypes_Brand_BrandId",
                table: "BusTypes",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id");
        }
    }
}
