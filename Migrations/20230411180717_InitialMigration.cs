using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kazariobranco_backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstname = table.Column<string>(type: "varchar(20)", nullable: false),
                    lastname = table.Column<string>(type: "varchar(20)", nullable: false),
                    cpf = table.Column<byte[]>(type: "varbinary(32)", nullable: false),
                    phone = table.Column<byte[]>(type: "varbinary(32)", nullable: false),
                    email = table.Column<byte[]>(type: "varbinary(32)", nullable: false),
                    birthday = table.Column<DateTime>(type: "date", nullable: false),
                    created_at = table.Column<DateTime>(type: "date", nullable: false),
                    updated_at = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_cpf",
                table: "users",
                column: "cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_phone",
                table: "users",
                column: "phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
