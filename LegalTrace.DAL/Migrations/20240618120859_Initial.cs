using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LegalTrace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "charges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FileLink = table.Column<string>(type: "text", nullable: false),
                    ChargeType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_charges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    TaxId = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Vigency = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SuperAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    Vigency = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "clientHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    EventDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Vigency = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_clientHistory_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "credentials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    Vigency = table.Column<bool>(type: "boolean", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    KeyValue = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_credentials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_credentials_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Repeatable = table.Column<bool>(type: "boolean", nullable: false),
                    Vigency = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Finished = table.Column<bool>(type: "boolean", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinishedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userTasks_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userTasks_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Address", "Created", "Email", "Name", "Password", "Phone", "SuperAdmin", "Updated", "Vigency" },
                values: new object[] { 1, "", new DateTime(2024, 6, 18, 12, 8, 59, 339, DateTimeKind.Utc).AddTicks(9141), "admin@admin.cl", "admin", "UzNLj3IXA35IMy1EL36TgsXvZsz0r5d1UtY4sAkDc5x+21PF", 0, true, new DateTime(2024, 6, 18, 12, 8, 59, 339, DateTimeKind.Utc).AddTicks(9143), true });

            migrationBuilder.CreateIndex(
                name: "IX_clientHistory_ClientId",
                table: "clientHistory",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_credentials_ClientId",
                table: "credentials",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_userTasks_ClientId",
                table: "userTasks",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_userTasks_UserId",
                table: "userTasks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "charges");

            migrationBuilder.DropTable(
                name: "clientHistory");

            migrationBuilder.DropTable(
                name: "credentials");

            migrationBuilder.DropTable(
                name: "userTasks");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
