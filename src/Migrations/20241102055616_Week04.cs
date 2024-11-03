using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace src.Migrations
{
    /// <inheritdoc />
    public partial class Week04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "066681de-3c0c-45e5-bb0b-fbc7fc556464");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18e66bee-d0ac-4ab2-a133-7b752c6e650e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f8d247a-b7ed-46c6-b341-60908c9d0794");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d91bc45-d5a1-4a45-b9f6-0501a0ce81bd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "825ed2a8-11e9-44e8-8141-667ee1cae21f");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7ca6cd18-bf91-45dc-97fc-d6e3cfc58f4f", null, "HRHead", "HRHEAD" },
                    { "b4a17ab5-e0c4-455c-baec-aa2788fb05ba", null, "Ceo", "CEO" },
                    { "e6aae43a-cfac-47c7-bb97-9bb0641deb90", null, "Employee", "EMPLOYEE" },
                    { "ebf5fa43-eb87-4c85-9087-bf85f2289529", null, "HRStaff", "HRSTAFF" },
                    { "efd18d8d-d5a9-45cf-a230-154eea759957", null, "DeptHead", "DEPTHEAD" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ca6cd18-bf91-45dc-97fc-d6e3cfc58f4f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4a17ab5-e0c4-455c-baec-aa2788fb05ba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e6aae43a-cfac-47c7-bb97-9bb0641deb90");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ebf5fa43-eb87-4c85-9087-bf85f2289529");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "efd18d8d-d5a9-45cf-a230-154eea759957");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "066681de-3c0c-45e5-bb0b-fbc7fc556464", null, "Ceo", "CEO" },
                    { "18e66bee-d0ac-4ab2-a133-7b752c6e650e", null, "Employee", "EMPLOYEE" },
                    { "6f8d247a-b7ed-46c6-b341-60908c9d0794", null, "HRHead", "HRHEAD" },
                    { "7d91bc45-d5a1-4a45-b9f6-0501a0ce81bd", null, "DeptHead", "DEPTHEAD" },
                    { "825ed2a8-11e9-44e8-8141-667ee1cae21f", null, "HRStaff", "HRSTAFF" }
                });
        }
    }
}
