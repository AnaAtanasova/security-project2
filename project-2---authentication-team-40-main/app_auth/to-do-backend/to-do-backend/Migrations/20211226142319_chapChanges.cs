using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace to_do_backend.Migrations
{
    public partial class chapChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "password");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "password");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "Users",
                type: "BLOB",
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
    }
}
