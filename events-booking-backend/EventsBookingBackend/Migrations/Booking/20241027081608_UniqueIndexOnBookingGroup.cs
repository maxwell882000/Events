using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsBookingBackend.Migrations.Booking
{
    /// <inheritdoc />
    public partial class UniqueIndexOnBookingGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_booking_groups_booking_type_id",
                schema: "bookings",
                table: "booking_groups");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "bookings",
                table: "booking_groups");

            migrationBuilder.CreateIndex(
                name: "ix_booking_groups_booking_type_id_event_id",
                schema: "bookings",
                table: "booking_groups",
                columns: new[] { "booking_type_id", "event_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_booking_groups_booking_type_id_event_id",
                schema: "bookings",
                table: "booking_groups");

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                schema: "bookings",
                table: "booking_groups",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_booking_groups_booking_type_id",
                schema: "bookings",
                table: "booking_groups",
                column: "booking_type_id");
        }
    }
}
