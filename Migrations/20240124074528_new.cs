using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMS.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentDetails_AspNetUsers_Id1",
                table: "StudentDetails");

            migrationBuilder.DropIndex(
                name: "IX_StudentDetails_Id1",
                table: "StudentDetails");

            migrationBuilder.DropColumn(
                name: "Id1",
                table: "StudentDetails");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "StudentDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_StudentDetails_UserId",
                table: "StudentDetails",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentDetails_AspNetUsers_UserId",
                table: "StudentDetails",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentDetails_AspNetUsers_UserId",
                table: "StudentDetails");

            migrationBuilder.DropIndex(
                name: "IX_StudentDetails_UserId",
                table: "StudentDetails");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudentDetails");

            migrationBuilder.AddColumn<string>(
                name: "Id1",
                table: "StudentDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentDetails_Id1",
                table: "StudentDetails",
                column: "Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentDetails_AspNetUsers_Id1",
                table: "StudentDetails",
                column: "Id1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
