﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movilissa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AmenityAndBusTypeModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BusTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Amenities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "BusTypes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Amenities");
        }
    }
}
