using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegalTrace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addduedateforusertask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "userTasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2024, 1, 19, 11, 29, 57, 577, DateTimeKind.Utc).AddTicks(3487), "E4tPiyqRjRS3Sy95SoyHBBFI2JzbYXVXo9TcltUZKJXN5yf0", new DateTime(2024, 1, 19, 11, 29, 57, 577, DateTimeKind.Utc).AddTicks(3489) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "userTasks");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2024, 1, 18, 13, 47, 49, 270, DateTimeKind.Utc).AddTicks(6060), "poDR1dm6XDWuEzs6pKB0vSMN4YX2AU0UZkFMIGQXxkQkL6jc", new DateTime(2024, 1, 18, 13, 47, 49, 270, DateTimeKind.Utc).AddTicks(6063) });
        }
    }
}
