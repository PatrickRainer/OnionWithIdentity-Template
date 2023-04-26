using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class ChangedUsertoOrgUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10a16ea8-6f30-486a-8489-2fd748702020");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a5ee487-ee4e-4b5c-9065-0f7f9771624d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "492fc1a0-c9f4-4a70-944a-db63eda8e36d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1b2bc5c6-3cfa-4127-9078-7580fd0b3399", "22e701c4-6bf6-41bc-b017-4b9f7f47df24", "OrgAdmin", "ORGADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dfd35852-7469-4452-96cb-e146209246df", "b5fdca4b-cf20-469c-b3ba-81c4c75e22e7", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fed9f464-6845-47fa-b677-c3a433d375a3", "d15633d3-4766-4f9d-a059-e331988e6086", "OrgUser", "ORGUSER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b2bc5c6-3cfa-4127-9078-7580fd0b3399");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfd35852-7469-4452-96cb-e146209246df");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fed9f464-6845-47fa-b677-c3a433d375a3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "10a16ea8-6f30-486a-8489-2fd748702020", "cc6d6382-5618-45b5-9f7c-c265f6590770", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3a5ee487-ee4e-4b5c-9065-0f7f9771624d", "f45e0e33-70d6-4ec4-9722-1c91a877b417", "OrgUser", "ORGUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "492fc1a0-c9f4-4a70-944a-db63eda8e36d", "9d314e81-255b-4c83-b711-16090a17ee7e", "OrgAdmin", "ORGADMIN" });
        }
    }
}
