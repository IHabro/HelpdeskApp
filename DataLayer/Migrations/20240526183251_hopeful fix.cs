using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class hopefulfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Projects_ActiveProjectId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ActiveProjectId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ActiveProjectId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ActiveProject_Fk",
                table: "AspNetUsers",
                column: "ActiveProject_Fk");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Projects_ActiveProject_Fk",
                table: "AspNetUsers",
                column: "ActiveProject_Fk",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Projects_ActiveProject_Fk",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ActiveProject_Fk",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "ActiveProjectId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ActiveProjectId",
                table: "AspNetUsers",
                column: "ActiveProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Projects_ActiveProjectId",
                table: "AspNetUsers",
                column: "ActiveProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
