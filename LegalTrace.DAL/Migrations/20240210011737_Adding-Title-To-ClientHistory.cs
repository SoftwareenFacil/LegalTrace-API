using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LegalTrace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddingTitleToClientHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "standardTask");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "clientHistory",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2024, 2, 10, 1, 17, 37, 433, DateTimeKind.Utc).AddTicks(9262), "tp2Be4Q2ne39zm4ftEPuI4xOPL+QOj0pcMZdBFTExz9KfdBE", new DateTime(2024, 2, 10, 1, 17, 37, 433, DateTimeKind.Utc).AddTicks(9266) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "clientHistory");

            migrationBuilder.CreateTable(
                name: "standardTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DaysToFinish = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Repeatable = table.Column<bool>(type: "boolean", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_standardTask", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2024, 1, 26, 16, 49, 53, 882, DateTimeKind.Utc).AddTicks(8329), "Tztnm+1zvLnaIVIPK/YQEsVWlqI0gMCTZ2rn8OIagJcVAIVi", new DateTime(2024, 1, 26, 16, 49, 53, 882, DateTimeKind.Utc).AddTicks(8331) });
        }
    }
}
