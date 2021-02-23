using Microsoft.EntityFrameworkCore.Migrations;

namespace Agro360.Migrations
{
    public partial class addNewColumntoInquiry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Inquiry",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Inquiry",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "FarmerReport",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inquiry_Id",
                table: "Inquiry",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiry_AspNetUsers_Id",
                table: "Inquiry",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FarmerReport_AspNetUsers_Id",
                table: "FarmerReport");

            migrationBuilder.DropForeignKey(
                name: "FK_Inquiry_AspNetUsers_Id",
                table: "Inquiry");

            migrationBuilder.DropIndex(
                name: "IX_Inquiry_Id",
                table: "Inquiry");

            migrationBuilder.DropIndex(
                name: "IX_FarmerReport_Id",
                table: "FarmerReport");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Inquiry");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Inquiry",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "FarmerReport",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
