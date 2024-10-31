using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsBookingBackend.Migrations.Booking
{
    /// <inheritdoc />
    public partial class AddedEventIdToBookingType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "category_id",
                schema: "bookings",
                table: "booking_types",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "event_id",
                schema: "bookings",
                table: "booking_types",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "event_id",
                schema: "bookings",
                table: "booking_types");

            migrationBuilder.AlterColumn<Guid>(
                name: "category_id",
                schema: "bookings",
                table: "booking_types",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }
    }
}
