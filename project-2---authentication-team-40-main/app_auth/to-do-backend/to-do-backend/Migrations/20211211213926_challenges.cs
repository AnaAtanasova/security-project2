using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace to_do_backend.Migrations
{
    public partial class challenges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Challenges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChallengeValue = table.Column<byte[]>(type: "BLOB", nullable: true),
                    IterationCount = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSuccess = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Challenges_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_UserId",
                table: "Challenges",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Challenges");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "Dya06vXfuz2DayaSWeglgAat/0aJDXgkJFJARW6jZjU=", new byte[] { 227, 238, 51, 97, 0, 69, 54, 50, 37, 26, 238, 64, 247, 35, 2, 166 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "4l7KgywR6+ByZR9m6hRQr2MqQsm/dS0O+hF4AyMOU10=", new byte[] { 99, 130, 157, 132, 250, 198, 116, 101, 51, 59, 179, 45, 38, 24, 90, 17 } });
        }
    }
}
