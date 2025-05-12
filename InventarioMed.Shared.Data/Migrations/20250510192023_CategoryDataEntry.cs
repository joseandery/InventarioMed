using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioMed.Shared.Data.Migrations
{
    /// <inheritdoc />
    public partial class CategoryDataEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Category", new[] { "Name", "EquipmentId" }, new object[] { "Monitoramento", 4 });
            migrationBuilder.InsertData("Category", new[] { "Name", "EquipmentId" }, new object[] { "Cuidados Intensivos", 4 });
            migrationBuilder.InsertData("Category", new[] { "Name", "EquipmentId" }, new object[] { "Emergência", 5 });
            migrationBuilder.InsertData("Category", new[] { "Name", "EquipmentId" }, new object[] { "Suporte Respiratório", 7 });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
