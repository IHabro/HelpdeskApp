using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class BigJointablesFinalized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionToProjectToRole",
                columns: table => new
                {
                    Action_Fk = table.Column<int>(type: "int", nullable: false),
                    Role_Fk = table.Column<int>(type: "int", nullable: false),
                    Project_Fk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionToProjectToRole", x => new { x.Action_Fk, x.Project_Fk, x.Role_Fk });
                    table.ForeignKey(
                        name: "FK_ActionToProjectToRole_Actions_Action_Fk",
                        column: x => x.Action_Fk,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionToProjectToRole_Projects_Project_Fk",
                        column: x => x.Project_Fk,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionToProjectToRole_Roles_Role_Fk",
                        column: x => x.Role_Fk,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToProjectToRole",
                columns: table => new
                {
                    User_Fk = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role_Fk = table.Column<int>(type: "int", nullable: false),
                    Project_Fk = table.Column<int>(type: "int", nullable: false),
                    GrantedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToProjectToRole", x => new { x.User_Fk, x.Project_Fk, x.Role_Fk });
                    table.ForeignKey(
                        name: "FK_UserToProjectToRole_AspNetUsers_User_Fk",
                        column: x => x.User_Fk,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToProjectToRole_Projects_Project_Fk",
                        column: x => x.Project_Fk,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToProjectToRole_Roles_Role_Fk",
                        column: x => x.Role_Fk,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionToProjectToRole_Project_Fk",
                table: "ActionToProjectToRole",
                column: "Project_Fk");

            migrationBuilder.CreateIndex(
                name: "IX_ActionToProjectToRole_Role_Fk",
                table: "ActionToProjectToRole",
                column: "Role_Fk");

            migrationBuilder.CreateIndex(
                name: "IX_UserToProjectToRole_Project_Fk",
                table: "UserToProjectToRole",
                column: "Project_Fk");

            migrationBuilder.CreateIndex(
                name: "IX_UserToProjectToRole_Role_Fk",
                table: "UserToProjectToRole",
                column: "Role_Fk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionToProjectToRole");

            migrationBuilder.DropTable(
                name: "UserToProjectToRole");
        }
    }
}
