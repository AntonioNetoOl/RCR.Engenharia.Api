using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RCR.Engenharia.Sgrh.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCpfUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Usuarios");
        }
    }
}
