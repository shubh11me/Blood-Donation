using Microsoft.EntityFrameworkCore.Migrations;

namespace Blood_Donation.Migrations
{
    public partial class bloodRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodRequests",
                columns: table => new
                {
                    requestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bloodId = table.Column<int>(type: "int", nullable: false),
                    forHospital = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    byUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodRequests", x => x.requestId);
                    table.ForeignKey(
                        name: "FK_BloodRequests_BloodGroups_bloodId",
                        column: x => x.bloodId,
                        principalTable: "BloodGroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BloodRequests_bloodId",
                table: "BloodRequests",
                column: "bloodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodRequests");
        }
    }
}
