using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsBookingBackend.Migrations.Telegram
{
    /// <inheritdoc />
    public partial class AddedChatIdUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_telegram_users_chat_id",
                schema: "telegram",
                table: "telegram_users",
                column: "chat_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_telegram_users_chat_id",
                schema: "telegram",
                table: "telegram_users");
        }
    }
}
