using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsBookingBackend.Migrations.Booking
{
    /// <inheritdoc />
    public partial class UniqueIndexOnChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_booking_groups_booking_type_id_event_id",
                schema: "bookings",
                table: "booking_groups");

            migrationBuilder.CreateIndex(
                name: "ix_booking_groups_booking_type_id_event_id_user_options",
                schema: "bookings",
                table: "booking_groups",
                columns: new[] { "booking_type_id", "event_id", "user_options" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_booking_groups_booking_type_id_event_id_user_options",
                schema: "bookings",
                table: "booking_groups");

            migrationBuilder.CreateIndex(
                name: "ix_booking_groups_booking_type_id_event_id",
                schema: "bookings",
                table: "booking_groups",
                columns: new[] { "booking_type_id", "event_id" },
                unique: true);
        }
    }
}
