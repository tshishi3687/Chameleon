using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chameleon.Migrations
{
    /// <inheritdoc />
    public partial class AddContactDetailsInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersContactDetails",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ContactDetailsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersContactDetails", x => new { x.ContactDetailsId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UsersContactDetails_ContactDetails_ContactDetailsId",
                        column: x => x.ContactDetailsId,
                        principalTable: "ContactDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersContactDetails_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UsersContactDetails_UserId",
                table: "UsersContactDetails",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersContactDetails");
        }
    }
}
