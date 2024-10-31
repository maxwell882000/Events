using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsBookingBackend.Migrations.Booking
{
    /// <inheritdoc />
    public partial class AddedToUniqueEventId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_booking_types_category_id_order",
                schema: "bookings",
                table: "booking_types");

            migrationBuilder.CreateIndex(
                name: "ix_booking_types_category_id_event_id_order",
                schema: "bookings",
                table: "booking_types",
                columns: new[] { "category_id", "event_id", "order" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_booking_types_category_id_event_id_order",
                schema: "bookings",
                table: "booking_types");

            migrationBuilder.CreateIndex(
                name: "ix_booking_types_category_id_order",
                schema: "bookings",
                table: "booking_types",
                columns: new[] { "category_id", "order" },
                unique: true);
        }
    }
}
