using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace to_do_backend.Migrations
{
    public partial class ChallengeStructuralChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "secret",
                table: "Challenges",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "HjnU0bYQ/rw4Y9+CKFRUH9XkrF1Zef4Ra1AadarLovE=", new byte[] { 136, 183, 252, 231, 172, 155, 175, 105, 15, 217, 233, 232, 37, 181, 159, 158 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "OUfLNgaLhylHqtNN/PAM0nRdfn7j5qBQtRqG0bSL09k=", new byte[] { 72, 151, 194, 17, 254, 151, 125, 72, 1, 14, 140, 16, 35, 168, 40, 61 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "secret",
                table: "Challenges");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "M7n6ABrQSKHECKAvNyB347WsvujibSlgSC1ggNPqMrU=", new byte[] { 145, 108, 1, 181, 165, 45, 143, 94, 215, 140, 108, 178, 113, 193, 62, 85 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "7CGQiBig1GRJbtw2pkXeJwgQ/YzqMntrSXthawl5AoM=", new byte[] { 3, 32, 24, 140, 90, 152, 49, 166, 228, 60, 44, 156, 20, 150, 12, 190 } });
        }
    }
}
