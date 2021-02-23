using Microsoft.EntityFrameworkCore.Migrations;

namespace Agro360.Migrations
{
    public partial class addInquiryTableFIx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Inquiry_ReportId",
                table: "Inquiry",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiry_FarmerReport_ReportId",
                table: "Inquiry",
                column: "ReportId",
                principalTable: "FarmerReport",
                principalColumn: "ReportId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiry_FarmerReport_ReportId",
                table: "Inquiry");

            migrationBuilder.DropIndex(
                name: "IX_Inquiry_ReportId",
                table: "Inquiry");
        }
    }
}
