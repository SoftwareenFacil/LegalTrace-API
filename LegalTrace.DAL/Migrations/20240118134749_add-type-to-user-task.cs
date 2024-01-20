using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegalTrace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addtypetousertask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "userTasks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2024, 1, 18, 13, 47, 49, 270, DateTimeKind.Utc).AddTicks(6060), "poDR1dm6XDWuEzs6pKB0vSMN4YX2AU0UZkFMIGQXxkQkL6jc", new DateTime(2024, 1, 18, 13, 47, 49, 270, DateTimeKind.Utc).AddTicks(6063) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "userTasks");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2024, 1, 15, 16, 32, 23, 188, DateTimeKind.Utc).AddTicks(6843), "e97NNWx3YF3Rfiew5JIlUFfb/6Pq1YDnEsQ0xjqKs47Ucc/r", new DateTime(2024, 1, 15, 16, 32, 23, 188, DateTimeKind.Utc).AddTicks(6846) });
        }
    }
}
