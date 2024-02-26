using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegalTrace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Addobjectvigency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Vigency",
                table: "clientHistory",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2024, 2, 26, 14, 46, 34, 614, DateTimeKind.Utc).AddTicks(9527), "fQOYLTD6xbVSvc8oc5dOctuDVxZNUjHPArc41YTWWw++dxcD", new DateTime(2024, 2, 26, 14, 46, 34, 614, DateTimeKind.Utc).AddTicks(9533) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vigency",
                table: "clientHistory");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2024, 2, 10, 9, 29, 6, 974, DateTimeKind.Utc).AddTicks(7005), "sm3zSas1zQTRWYK5Tv18oFTVDDaDa8JtkE6AcuQ/xegqmTkr", new DateTime(2024, 2, 10, 9, 29, 6, 974, DateTimeKind.Utc).AddTicks(7012) });
        }
    }
}
