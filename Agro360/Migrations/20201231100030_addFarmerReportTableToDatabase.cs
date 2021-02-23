using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Agro360.Migrations
{
    public partial class addFarmerReportTableToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FarmerReport",
                columns: table => new
                {
                    ReportId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ReportState = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    HarvestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmerReport", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_FarmerReport_HarvestType_HarvestId",
                        column: x => x.HarvestId,
                        principalTable: "HarvestType",
                        principalColumn: "HarvestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FarmerReport_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FarmerReport_HarvestId",
                table: "FarmerReport",
                column: "HarvestId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmerReport_Id",
                table: "FarmerReport",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FarmerReport");
        }
    }
}
