using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chameleon.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueRoleName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Roles_Name",
                table: "Roles");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "longtext",
                nullable: false,
                defaultValue: "CUSTOMER",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldDefaultValue: "CUSTOMER")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "CUSTOMER",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldDefaultValue: "CUSTOMER")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);
        }
    }
}
