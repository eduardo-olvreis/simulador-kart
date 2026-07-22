using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartSimulator.Migrations
{
    /// <inheritdoc />
    public partial class AjustaEntitiesEValidacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pilotos_VeiculoId",
                table: "Pilotos");

            migrationBuilder.CreateIndex(
                name: "IX_Pilotos_VeiculoId",
                table: "Pilotos",
                column: "VeiculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pilotos_VeiculoId",
                table: "Pilotos");

            migrationBuilder.CreateIndex(
                name: "IX_Pilotos_VeiculoId",
                table: "Pilotos",
                column: "VeiculoId",
                unique: true);
        }
    }
}
