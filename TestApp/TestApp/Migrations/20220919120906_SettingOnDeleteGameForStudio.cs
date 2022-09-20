using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApp.Migrations
{
    public partial class SettingOnDeleteGameForStudio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_DeveloperStudios_DeveloperStudioId",
                table: "Games");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_DeveloperStudios_DeveloperStudioId",
                table: "Games",
                column: "DeveloperStudioId",
                principalTable: "DeveloperStudios",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_DeveloperStudios_DeveloperStudioId",
                table: "Games");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_DeveloperStudios_DeveloperStudioId",
                table: "Games",
                column: "DeveloperStudioId",
                principalTable: "DeveloperStudios",
                principalColumn: "Id");
        }
    }
}
