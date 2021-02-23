using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Agro360.Migrations
{
    public partial class addInquiryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FarmerReport_AspNetUsers_Id",
                table: "FarmerReport");

            migrationBuilder.DropIndex(
                name: "IX_FarmerReport_Id",
                table: "FarmerReport");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "FarmerReport",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Inquiry",
                columns: table => new
                {
                    InquiryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    InquiryState = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquiry", x => x.InquiryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inquiry");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "FarmerReport",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FarmerReport_Id",
                table: "FarmerReport",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FarmerReport_AspNetUsers_Id",
                table: "FarmerReport",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
