using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioMed.Shared.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovingCategoryDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
