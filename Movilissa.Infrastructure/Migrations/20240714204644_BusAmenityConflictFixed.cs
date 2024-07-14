using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movilissa_api.Migrations
{
    /// <inheritdoc />
    public partial class BusAmenityConflictFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BusAmenity",
                table: "BusAmenity");

            migrationBuilder.DropIndex(
                name: "IX_BusAmenity_BusId",
                table: "BusAmenity");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "BusAmenity",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusAmenity",
                table: "BusAmenity",
                columns: new[] { "BusId", "AmenityId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BusAmenity",
                table: "BusAmenity");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "BusAmenity",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusAmenity",
                table: "BusAmenity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BusAmenity_BusId",
                table: "BusAmenity",
                column: "BusId");
        }
    }
}
