using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdatedDate", "UserName" },
                values: new object[] { new Guid("cd2a4fee-8aa1-4756-8d84-f878f286fdbf"), 0, "36374cfb-1243-4e5a-9f04-4eef66e7359f", new DateTime(2024, 12, 29, 10, 56, 10, 503, DateTimeKind.Utc).AddTicks(2091), "superadmin@admin.com", false, false, null, "Sourav Ganguly", "SUPERADMIN@ADMIN.COM", "SUPERADMIN", "AQAAAAIAAYagAAAAEOIgnixcXdVKJobE89qxmwQQWD+GEKkXpWX1HDu+kI6yFAeg6aUYa6KdrGIuHnp2EQ==", null, false, "cd2a4fee-8aa1-4756-8d84-f878f286fdbf", false, new DateTime(2024, 12, 29, 10, 56, 10, 503, DateTimeKind.Utc).AddTicks(2093), "SuperAdmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cd2a4fee-8aa1-4756-8d84-f878f286fdbf"));
        }
    }
}
