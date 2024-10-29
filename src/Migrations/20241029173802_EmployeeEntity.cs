using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace src.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "232220cd-f207-451a-9060-848785b6d816");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ccc4006-1834-450f-b822-6a8ea9e66c11");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b1fd30f-d625-48d6-9c2b-12eaeb8d12c4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b58c5bf1-2e0b-408c-9eaa-ad074e512a08");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2dae6b9-da2d-4c26-a4e0-8524bf1ad212");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "232220cd-f207-451a-9060-848785b6d816", null, "Ceo", "CEO" },
                    { "8ccc4006-1834-450f-b822-6a8ea9e66c11", null, "HRHead", "HRHEAD" },
                    { "9b1fd30f-d625-48d6-9c2b-12eaeb8d12c4", null, "HRStaff", "HRSTAFF" },
                    { "b58c5bf1-2e0b-408c-9eaa-ad074e512a08", null, "Employee", "EMPLOYEE" },
                    { "d2dae6b9-da2d-4c26-a4e0-8524bf1ad212", null, "DeptHead", "DEPTHEAD" }
                });
        }
    }
}
