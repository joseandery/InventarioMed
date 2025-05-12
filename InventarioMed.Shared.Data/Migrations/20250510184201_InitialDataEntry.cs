using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioMed.Shared.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Equipment", new[] { "Name", "Manufacturer" }, new object[] { "Monitor multiparamétrico", "Philips" });
            migrationBuilder.InsertData("Equipment", new[] { "Name", "Manufacturer" }, new object[] { "Bomba de Infusão", "Abbott" });
            migrationBuilder.InsertData("Equipment", new[] { "Name", "Manufacturer" }, new object[] { "Ventilador Pulmonar", "Dräger" });
            migrationBuilder.InsertData("Equipment", new[] { "Name", "Manufacturer" }, new object[] { "Desfibrilador Externo", "Zoll" });
            migrationBuilder.InsertData("Equipment", new[] { "Name", "Manufacturer" }, new object[] { "ECG Portátil", "GE Healthcare" });
            migrationBuilder.InsertData("Equipment", new[] { "Name", "Manufacturer" }, new object[] { "Oxímetro de Pulso", "Contec" });
            migrationBuilder.InsertData("Equipment", new[] { "Name", "Manufacturer" }, new object[] { "Aspirador Cirúrgico", "MedVac" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Equipment");
        }
    }
}
