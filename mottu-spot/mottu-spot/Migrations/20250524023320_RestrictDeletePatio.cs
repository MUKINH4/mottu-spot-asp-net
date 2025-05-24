using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mottu_spot.Migrations
{
    /// <inheritdoc />
    public partial class RestrictDeletePatio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motos_Patios_PatioId",
                table: "Motos");

            migrationBuilder.AddForeignKey(
                name: "FK_Motos_Patios_PatioId",
                table: "Motos",
                column: "PatioId",
                principalTable: "Patios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motos_Patios_PatioId",
                table: "Motos");

            migrationBuilder.AddForeignKey(
                name: "FK_Motos_Patios_PatioId",
                table: "Motos",
                column: "PatioId",
                principalTable: "Patios",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
