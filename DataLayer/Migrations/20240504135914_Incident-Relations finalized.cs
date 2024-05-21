using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class IncidentRelationsfinalized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "Incidents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level_Fk",
                table: "Incidents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Incidents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Project_Fk",
                table: "Incidents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Incidents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User_Fk",
                table: "Incidents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_LevelId",
                table: "Incidents",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_ProjectId",
                table: "Incidents",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_UserId",
                table: "Incidents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_AspNetUsers_UserId",
                table: "Incidents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Levels_LevelId",
                table: "Incidents",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Projects_ProjectId",
                table: "Incidents",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_AspNetUsers_UserId",
                table: "Incidents");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Levels_LevelId",
                table: "Incidents");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Projects_ProjectId",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_LevelId",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_ProjectId",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_UserId",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "Level_Fk",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "Project_Fk",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "User_Fk",
                table: "Incidents");
        }
    }
}
