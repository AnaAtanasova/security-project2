using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace to_do_backend.Migrations
{
    public partial class challengesChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSuccess",
                table: "Challenges",
                newName: "IsFailed");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFailed",
                table: "Challenges",
                newName: "IsSuccess");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "L8hsfqz+Mei6Lp4mc+/P/OTwjEOCnCL9+4hRr7US3zc=", new byte[] { 237, 101, 220, 129, 93, 213, 18, 155, 133, 100, 5, 203, 252, 208, 230, 63 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "kHg0hfsHoLn/31KMXfft8BOsqtrSENVIJ+vbVsZJ+pI=", new byte[] { 206, 35, 142, 156, 253, 173, 142, 16, 6, 80, 76, 114, 52, 110, 152, 62 } });
        }
    }
}
