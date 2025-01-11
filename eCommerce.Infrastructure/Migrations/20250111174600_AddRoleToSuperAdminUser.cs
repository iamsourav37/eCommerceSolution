using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleToSuperAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0282f81e-5691-4784-ab89-c3d0a494e8c3"), new Guid("cd2a4fee-8aa1-4756-8d84-f878f286fdbf") },
                    { new Guid("367feff3-0ff0-4feb-8451-16c04bdce883"), new Guid("cd2a4fee-8aa1-4756-8d84-f878f286fdbf") },
                    { new Guid("6f2295d4-bcf7-447a-a1ff-941daa524cab"), new Guid("cd2a4fee-8aa1-4756-8d84-f878f286fdbf") }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cd2a4fee-8aa1-4756-8d84-f878f286fdbf"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "816938a7-b918-4b26-8779-31d21b84c04c", "AQAAAAIAAYagAAAAEBYOdakC0FX9L9YWH3KhVwWIyX6cytsJLwVXuuNl5wrakMuvR3c9fVi5/pSjCSk9Qw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("0282f81e-5691-4784-ab89-c3d0a494e8c3"), new Guid("cd2a4fee-8aa1-4756-8d84-f878f286fdbf") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("367feff3-0ff0-4feb-8451-16c04bdce883"), new Guid("cd2a4fee-8aa1-4756-8d84-f878f286fdbf") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("6f2295d4-bcf7-447a-a1ff-941daa524cab"), new Guid("cd2a4fee-8aa1-4756-8d84-f878f286fdbf") });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cd2a4fee-8aa1-4756-8d84-f878f286fdbf"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "832ec862-7a00-4a4e-b68f-f44ed57dc2c4", "AQAAAAIAAYagAAAAEJM1BcEGEPGc+f87fYs5mqvgV46hMpIzTMFIgI1pV6LmBqIlcKcYDRyxnaMgQDM9aw==" });
        }
    }
}
