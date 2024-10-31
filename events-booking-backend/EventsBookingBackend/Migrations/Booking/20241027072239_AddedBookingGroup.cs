using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsBookingBackend.Migrations.Booking
{
    /// <inheritdoc />
    public partial class AddedBookingGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "booking_group_id",
                schema: "bookings",
                table: "bookings",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "duration_in_days",
                schema: "bookings",
                table: "booking_types",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "booking_groups",
                schema: "bookings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    event_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    booking_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    user_options = table.Column<string>(type: "text", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_booking_groups", x => x.id);
                    table.ForeignKey(
                        name: "fk_booking_groups_booking_types_booking_type_id",
                        column: x => x.booking_type_id,
                        principalSchema: "bookings",
                        principalTable: "booking_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_bookings_booking_group_id",
                schema: "bookings",
                table: "bookings",
                column: "booking_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_booking_groups_booking_type_id",
                schema: "bookings",
                table: "booking_groups",
                column: "booking_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_bookings_booking_groups_booking_group_id",
                schema: "bookings",
                table: "bookings",
                column: "booking_group_id",
                principalSchema: "bookings",
                principalTable: "booking_groups",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_bookings_booking_groups_booking_group_id",
                schema: "bookings",
                table: "bookings");

            migrationBuilder.DropTable(
                name: "booking_groups",
                schema: "bookings");

            migrationBuilder.DropIndex(
                name: "ix_bookings_booking_group_id",
                schema: "bookings",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "booking_group_id",
                schema: "bookings",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "duration_in_days",
                schema: "bookings",
                table: "booking_types");
        }
    }
}
