using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegalTrace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addchargeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChargeType",
                table: "charges",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2024, 4, 8, 13, 54, 7, 791, DateTimeKind.Utc).AddTicks(5020), "jd+zH6H8nuEWQkio/Xd4kNNdKyYKpuNR6PwNXzYQGFdCXNo7", new DateTime(2024, 4, 8, 13, 54, 7, 791, DateTimeKind.Utc).AddTicks(5022) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargeType",
                table: "charges");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2024, 3, 18, 12, 44, 30, 929, DateTimeKind.Utc).AddTicks(4921), "e0KaCk2s6ZFpw7pI1Nlf1gZuZWBI3okdn1NzF+hqMNlVvUkJ", new DateTime(2024, 3, 18, 12, 44, 30, 929, DateTimeKind.Utc).AddTicks(4922) });
        }
    }
}
