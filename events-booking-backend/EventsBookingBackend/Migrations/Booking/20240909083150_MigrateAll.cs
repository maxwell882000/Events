using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EventsBookingBackend.Migrations.Booking
{
    /// <inheritdoc />
    public partial class MigrateAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bookings");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "booking_events",
                schema: "bookings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_booking_events", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "booking_types",
                schema: "bookings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    label = table.Column<string>(type: "text", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cost = table.Column<decimal>(type: "numeric", nullable: false),
                    icon_path = table.Column<string>(type: "text", nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false),
                    is_show_limit = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_booking_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "booking_limits",
                schema: "bookings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    event_id = table.Column<Guid>(type: "uuid", nullable: true),
                    booking_type_id = table.Column<Guid>(type: "uuid", nullable: true),
                    max_bookings = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_booking_limits", x => x.id);
                    table.ForeignKey(
                        name: "fk_booking_limits_booking_types_booking_type_id",
                        column: x => x.booking_type_id,
                        principalSchema: "bookings",
                        principalTable: "booking_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "booking_options",
                schema: "bookings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    label = table.Column<string>(type: "text", nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    booking_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_booking_options", x => x.id);
                    table.ForeignKey(
                        name: "fk_booking_options_booking_types_booking_type_id",
                        column: x => x.booking_type_id,
                        principalSchema: "bookings",
                        principalTable: "booking_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                schema: "bookings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    status = table.Column<string>(type: "text", nullable: false),
                    event_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    booking_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bookings", x => x.id);
                    table.ForeignKey(
                        name: "fk_bookings_booking_events_event_id",
                        column: x => x.event_id,
                        principalSchema: "bookings",
                        principalTable: "booking_events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_bookings_booking_types_booking_type_id",
                        column: x => x.booking_type_id,
                        principalSchema: "bookings",
                        principalTable: "booking_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "booking_options_booking_option_values",
                schema: "bookings",
                columns: table => new
                {
                    booking_option_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<string>(type: "text", nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_booking_options_booking_option_values", x => new { x.booking_option_id, x.id });
                    table.ForeignKey(
                        name: "fk_booking_options_booking_option_values_booking_options_booki",
                        column: x => x.booking_option_id,
                        principalSchema: "bookings",
                        principalTable: "booking_options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "booking_user_options",
                schema: "bookings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    booking_option_value_value = table.Column<string>(type: "text", nullable: false),
                    booking_option_value_order = table.Column<int>(type: "integer", nullable: false),
                    booking_id = table.Column<Guid>(type: "uuid", nullable: false),
                    option_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_booking_user_options", x => x.id);
                    table.ForeignKey(
                        name: "fk_booking_user_options_booking_options_option_id",
                        column: x => x.option_id,
                        principalSchema: "bookings",
                        principalTable: "booking_options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_booking_user_options_bookings_booking_id",
                        column: x => x.booking_id,
                        principalSchema: "bookings",
                        principalTable: "bookings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_booking_events_is_deleted",
                schema: "bookings",
                table: "booking_events",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "ix_booking_limits_booking_type_id",
                schema: "bookings",
                table: "booking_limits",
                column: "booking_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_booking_limits_is_deleted",
                schema: "bookings",
                table: "booking_limits",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "ix_booking_options_booking_type_id_order",
                schema: "bookings",
                table: "booking_options",
                columns: new[] { "booking_type_id", "order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_booking_options_is_deleted",
                schema: "bookings",
                table: "booking_options",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "ix_booking_types_category_id_order",
                schema: "bookings",
                table: "booking_types",
                columns: new[] { "category_id", "order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_booking_types_is_deleted",
                schema: "bookings",
                table: "booking_types",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "ix_booking_user_options_booking_id",
                schema: "bookings",
                table: "booking_user_options",
                column: "booking_id");

            migrationBuilder.CreateIndex(
                name: "ix_booking_user_options_is_deleted",
                schema: "bookings",
                table: "booking_user_options",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "ix_booking_user_options_option_id",
                schema: "bookings",
                table: "booking_user_options",
                column: "option_id");

            migrationBuilder.CreateIndex(
                name: "ix_bookings_booking_type_id",
                schema: "bookings",
                table: "bookings",
                column: "booking_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_bookings_event_id",
                schema: "bookings",
                table: "bookings",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "ix_bookings_is_deleted",
                schema: "bookings",
                table: "bookings",
                column: "is_deleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booking_limits",
                schema: "bookings");

            migrationBuilder.DropTable(
                name: "booking_options_booking_option_values",
                schema: "bookings");

            migrationBuilder.DropTable(
                name: "booking_user_options",
                schema: "bookings");

            migrationBuilder.DropTable(
                name: "booking_options",
                schema: "bookings");

            migrationBuilder.DropTable(
                name: "bookings",
                schema: "bookings");

            migrationBuilder.DropTable(
                name: "booking_events",
                schema: "bookings");

            migrationBuilder.DropTable(
                name: "booking_types",
                schema: "bookings");
        }
    }
}
