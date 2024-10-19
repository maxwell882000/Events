using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EventsBookingBackend.Migrations.Payme
{
    /// <inheritdoc />
    public partial class MigrateAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "payme");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "transaction_details",
                schema: "payme",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    payme_id = table.Column<string>(type: "text", nullable: false),
                    time = table.Column<long>(type: "bigint", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    account_booking_id = table.Column<Guid>(type: "uuid", nullable: true),
                    create_time = table.Column<long>(type: "bigint", nullable: false),
                    perform_time = table.Column<long>(type: "bigint", nullable: false),
                    cancel_time = table.Column<long>(type: "bigint", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    reason = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transaction_details", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "receiver",
                schema: "payme",
                columns: table => new
                {
                    transaction_detail_account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_receiver", x => new { x.transaction_detail_account_id, x.id });
                    table.ForeignKey(
                        name: "fk_receiver_transaction_details_transaction_detail_account_id",
                        column: x => x.transaction_detail_account_id,
                        principalSchema: "payme",
                        principalTable: "transaction_details",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_transaction_details_is_deleted",
                schema: "payme",
                table: "transaction_details",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "ix_transaction_details_payme_id",
                schema: "payme",
                table: "transaction_details",
                column: "payme_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_transaction_details_time",
                schema: "payme",
                table: "transaction_details",
                column: "time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "receiver",
                schema: "payme");

            migrationBuilder.DropTable(
                name: "transaction_details",
                schema: "payme");
        }
    }
}
