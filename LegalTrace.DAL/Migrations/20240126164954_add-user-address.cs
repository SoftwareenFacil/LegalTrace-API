using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegalTrace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class adduseraddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_standardTask_clients_ClientId",
                table: "standardTask");

            migrationBuilder.DropIndex(
                name: "IX_standardTask_ClientId",
                table: "standardTask");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Created", "Password", "Updated" },
                values: new object[] { "", new DateTime(2024, 1, 26, 16, 49, 53, 882, DateTimeKind.Utc).AddTicks(8329), "Tztnm+1zvLnaIVIPK/YQEsVWlqI0gMCTZ2rn8OIagJcVAIVi", new DateTime(2024, 1, 26, 16, 49, 53, 882, DateTimeKind.Utc).AddTicks(8331) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "users");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2024, 1, 19, 11, 29, 57, 577, DateTimeKind.Utc).AddTicks(3487), "E4tPiyqRjRS3Sy95SoyHBBFI2JzbYXVXo9TcltUZKJXN5yf0", new DateTime(2024, 1, 19, 11, 29, 57, 577, DateTimeKind.Utc).AddTicks(3489) });

            migrationBuilder.CreateIndex(
                name: "IX_standardTask_ClientId",
                table: "standardTask",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_standardTask_clients_ClientId",
                table: "standardTask",
                column: "ClientId",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
