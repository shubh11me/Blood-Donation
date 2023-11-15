using Microsoft.EntityFrameworkCore.Migrations;

namespace Blood_Donation.Migrations
{
    public partial class idenUserTableRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUser<string>",
                table: "IdentityUser<string>");

            migrationBuilder.RenameTable(
                name: "IdentityUser<string>",
                newName: "IdentityUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUser",
                table: "IdentityUser",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUser",
                table: "IdentityUser");

            migrationBuilder.RenameTable(
                name: "IdentityUser",
                newName: "IdentityUser<string>");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUser<string>",
                table: "IdentityUser<string>",
                column: "Id");
        }
    }
}
