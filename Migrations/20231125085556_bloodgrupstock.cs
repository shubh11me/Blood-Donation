using Microsoft.EntityFrameworkCore.Migrations;

namespace Blood_Donation.Migrations
{
    public partial class bloodgrupstock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodGroupStocks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bloodStockBlood = table.Column<int>(type: "int", nullable: false),
                    bloodStockQuantity = table.Column<int>(type: "int", nullable: false),
                    bloodStockHospital = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodGroupStocks", x => x.id);
                    table.ForeignKey(
                        name: "FK_BloodGroupStocks_BloodGroups_bloodStockBlood",
                        column: x => x.bloodStockBlood,
                        principalTable: "BloodGroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BloodGroupStocks_bloodStockBlood",
                table: "BloodGroupStocks",
                column: "bloodStockBlood");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodGroupStocks");
        }
    }
}
