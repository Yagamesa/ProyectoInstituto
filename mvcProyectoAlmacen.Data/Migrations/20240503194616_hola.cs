using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvcProyectoAlmacen.Data.Migrations
{
    /// <inheritdoc />
    public partial class hola : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materia_Colegio_ColegioId",
                table: "Materia");

            migrationBuilder.DropIndex(
                name: "IX_Materia_ColegioId",
                table: "Materia");

            migrationBuilder.DropColumn(
                name: "ColegioId",
                table: "Materia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColegioId",
                table: "Materia",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Materia_ColegioId",
                table: "Materia",
                column: "ColegioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materia_Colegio_ColegioId",
                table: "Materia",
                column: "ColegioId",
                principalTable: "Colegio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
