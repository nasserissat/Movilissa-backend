using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movilissa_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Countries_CountryId",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Invoices_InvoiceId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Branches_DestinationId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Buses_BusId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_BusId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Payments_InvoiceId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Branches_CountryId",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_Amenities_BusId",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "ArrivalDateTime",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "BusId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Fare",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Buses");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "Tel",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "BusId",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Amenities");

            migrationBuilder.RenameColumn(
                name: "DepartureDateTime",
                table: "Schedules",
                newName: "DepartureTime");

            migrationBuilder.RenameColumn(
                name: "DestinationId",
                table: "Routes",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_DestinationId",
                table: "Routes",
                newName: "IX_Routes_CompanyId");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "Payments",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "OaymentId",
                table: "Invoices",
                newName: "PaymentId");

            migrationBuilder.RenameColumn(
                name: "SeatingCapacity",
                table: "Buses",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "Model",
                table: "Buses",
                newName: "LicensePlate");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Branches",
                newName: "Status");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalTime",
                table: "Schedules",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DaysOfWeek",
                table: "Schedules",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "EstimatedDuration",
                table: "Schedules",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "InvoiceDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "Companies",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "Companies",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Instagram",
                table: "Companies",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Facebook",
                table: "Companies",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Companies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tel",
                table: "Companies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "BusTypeId",
                table: "Buses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Amenities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BusAmenity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BusId = table.Column<int>(type: "int", nullable: false),
                    AmenityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusAmenity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusAmenity_Amenities_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "Amenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusAmenity_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BusSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BusId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusSchedules_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusSchedules_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BusStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusStatus", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BusType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Brand = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Model = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SeatingCapacity = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusType_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RouteDestination",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteDestination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteDestination_Branches_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteDestination_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                table: "Users",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CompanyId",
                table: "Tickets",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CompanyId",
                table: "Payments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CompanyId",
                table: "Invoices",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentId",
                table: "Invoices",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_CompanyId",
                table: "InvoiceDetails",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_BusTypeId",
                table: "Buses",
                column: "BusTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_StatusId",
                table: "Buses",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_CompanyId",
                table: "Amenities",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BusAmenity_AmenityId",
                table: "BusAmenity",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_BusAmenity_BusId",
                table: "BusAmenity",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_BusSchedules_BusId",
                table: "BusSchedules",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_BusSchedules_ScheduleId",
                table: "BusSchedules",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_BusType_CompanyId",
                table: "BusType",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteDestination_DestinationId",
                table: "RouteDestination",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteDestination_RouteId",
                table: "RouteDestination",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Companies_CompanyId",
                table: "Amenities",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_BusStatus_StatusId",
                table: "Buses",
                column: "StatusId",
                principalTable: "BusStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_BusType_BusTypeId",
                table: "Buses",
                column: "BusTypeId",
                principalTable: "BusType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_Companies_CompanyId",
                table: "InvoiceDetails",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Companies_CompanyId",
                table: "Invoices",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Payments_PaymentId",
                table: "Invoices",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Companies_CompanyId",
                table: "Payments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Companies_CompanyId",
                table: "Routes",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Companies_CompanyId",
                table: "Tickets",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Companies_CompanyId",
                table: "Users",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Companies_CompanyId",
                table: "Amenities");

            migrationBuilder.DropForeignKey(
                name: "FK_Buses_BusStatus_StatusId",
                table: "Buses");

            migrationBuilder.DropForeignKey(
                name: "FK_Buses_BusType_BusTypeId",
                table: "Buses");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_Companies_CompanyId",
                table: "InvoiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Companies_CompanyId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Payments_PaymentId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Companies_CompanyId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Companies_CompanyId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Companies_CompanyId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Companies_CompanyId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "BusAmenity");

            migrationBuilder.DropTable(
                name: "BusSchedules");

            migrationBuilder.DropTable(
                name: "BusStatus");

            migrationBuilder.DropTable(
                name: "BusType");

            migrationBuilder.DropTable(
                name: "RouteDestination");

            migrationBuilder.DropIndex(
                name: "IX_Users_CompanyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CompanyId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Payments_CompanyId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CompanyId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_PaymentId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDetails_CompanyId",
                table: "InvoiceDetails");

            migrationBuilder.DropIndex(
                name: "IX_Buses_BusTypeId",
                table: "Buses");

            migrationBuilder.DropIndex(
                name: "IX_Buses_StatusId",
                table: "Buses");

            migrationBuilder.DropIndex(
                name: "IX_Amenities_CompanyId",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "DaysOfWeek",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "EstimatedDuration",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Tel",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "BusTypeId",
                table: "Buses");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Amenities");

            migrationBuilder.RenameColumn(
                name: "DepartureTime",
                table: "Schedules",
                newName: "DepartureDateTime");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Routes",
                newName: "DestinationId");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_CompanyId",
                table: "Routes",
                newName: "IX_Routes_DestinationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Payments",
                newName: "InvoiceId");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "Invoices",
                newName: "OaymentId");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Buses",
                newName: "SeatingCapacity");

            migrationBuilder.RenameColumn(
                name: "LicensePlate",
                table: "Buses",
                newName: "Model");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Branches",
                newName: "CountryId");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalDateTime",
                table: "Schedules",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "BusId",
                table: "Schedules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Fare",
                table: "Schedules",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Website",
                keyValue: null,
                column: "Website",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "Companies",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Logo",
                keyValue: null,
                column: "Logo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "Companies",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Instagram",
                keyValue: null,
                column: "Instagram",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Instagram",
                table: "Companies",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Facebook",
                keyValue: null,
                column: "Facebook",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Facebook",
                table: "Companies",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Buses",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Branches",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Tel",
                table: "Branches",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "BusId",
                table: "Amenities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Amenities",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_BusId",
                table: "Schedules",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoiceId",
                table: "Payments",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CountryId",
                table: "Branches",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_BusId",
                table: "Amenities",
                column: "BusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Buses_BusId",
                table: "Amenities",
                column: "BusId",
                principalTable: "Buses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Countries_CountryId",
                table: "Branches",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Invoices_InvoiceId",
                table: "Payments",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Branches_DestinationId",
                table: "Routes",
                column: "DestinationId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Buses_BusId",
                table: "Schedules",
                column: "BusId",
                principalTable: "Buses",
                principalColumn: "Id");
        }
    }
}
