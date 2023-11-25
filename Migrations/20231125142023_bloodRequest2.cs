using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blood_Donation.Migrations
{
    public partial class bloodRequest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "req_status",
                table: "BloodRequests",
                type: "int",
                nullable: false,
                defaultValue:2);

            migrationBuilder.AddColumn<DateTime>(
                name: "req_time",
                table: "BloodRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "req_status",
                table: "BloodRequests");

            migrationBuilder.DropColumn(
                name: "req_time",
                table: "BloodRequests");
        }
    }
}
