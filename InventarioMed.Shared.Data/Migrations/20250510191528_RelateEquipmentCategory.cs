using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioMed.Shared.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelateEquipmentCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_EquipmentId",
                table: "Category",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Equipment_EquipmentId",
                table: "Category",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Equipment_EquipmentId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_EquipmentId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "Category");
        }
    }
}
