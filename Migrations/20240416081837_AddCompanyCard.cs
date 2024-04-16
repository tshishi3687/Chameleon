using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chameleon.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyIGuid",
                table: "Card",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "CompanyCards",
                columns: table => new
                {
                    CompanyGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CardGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CompanyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CardId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCards", x => new { x.CompanyGuid, x.CardGuid });
                    table.ForeignKey(
                        name: "FK_CompanyCards_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyCards_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCards_CardId",
                table: "CompanyCards",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCards_CompanyId",
                table: "CompanyCards",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyCards");

            migrationBuilder.DropColumn(
                name: "CompanyIGuid",
                table: "Card");
        }
    }
}
