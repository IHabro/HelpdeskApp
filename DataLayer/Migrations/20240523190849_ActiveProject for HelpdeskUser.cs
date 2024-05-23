using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ActiveProjectforHelpdeskUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActiveProjectId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActiveProject_Fk",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "ActiveProject_Fk",
                table: "AspNetUsers");
        }
    }
}
