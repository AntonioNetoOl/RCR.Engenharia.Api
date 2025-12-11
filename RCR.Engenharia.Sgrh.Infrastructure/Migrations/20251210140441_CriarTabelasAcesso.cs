using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RCR.Engenharia.Sgrh.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelasAcesso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Funcionarios",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "NomeCompleto",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "Setor",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Funcionarios");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Funcionarios",
                newName: "funcionarios",
                newSchema: "dbo");

            migrationBuilder.RenameColumn(
                name: "Cargo",
                schema: "dbo",
                table: "funcionarios",
                newName: "cargo");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "funcionarios",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "dbo",
                table: "funcionarios",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "DataAdmissao",
                schema: "dbo",
                table: "funcionarios",
                newName: "criado_em");

            migrationBuilder.AlterColumn<string>(
                name: "cargo",
                schema: "dbo",
                table: "funcionarios",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ativo",
                schema: "dbo",
                table: "funcionarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "atualizado_em",
                schema: "dbo",
                table: "funcionarios",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "data_nascimento",
                schema: "dbo",
                table: "funcionarios",
                type: "date",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_funcionarios",
                schema: "dbo",
                table: "funcionarios",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Perfis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EhAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Grupo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerfilId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Perfis_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "Perfis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerfilPermissoes",
                columns: table => new
                {
                    PerfisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissoesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilPermissoes", x => new { x.PerfisId, x.PermissoesId });
                    table.ForeignKey(
                        name: "FK_PerfilPermissoes_Perfis_PerfisId",
                        column: x => x.PerfisId,
                        principalTable: "Perfis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerfilPermissoes_Permissoes_PermissoesId",
                        column: x => x.PermissoesId,
                        principalTable: "Permissoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerfilPermissoes_PermissoesId",
                table: "PerfilPermissoes",
                column: "PermissoesId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissoes_Slug",
                table: "Permissoes",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PerfilId",
                table: "Usuarios",
                column: "PerfilId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerfilPermissoes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Permissoes");

            migrationBuilder.DropTable(
                name: "Perfis");

            migrationBuilder.DropPrimaryKey(
                name: "PK_funcionarios",
                schema: "dbo",
                table: "funcionarios");

            migrationBuilder.DropColumn(
                name: "ativo",
                schema: "dbo",
                table: "funcionarios");

            migrationBuilder.DropColumn(
                name: "atualizado_em",
                schema: "dbo",
                table: "funcionarios");

            migrationBuilder.DropColumn(
                name: "data_nascimento",
                schema: "dbo",
                table: "funcionarios");

            migrationBuilder.RenameTable(
                name: "funcionarios",
                schema: "dbo",
                newName: "Funcionarios");

            migrationBuilder.RenameColumn(
                name: "cargo",
                table: "Funcionarios",
                newName: "Cargo");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Funcionarios",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Funcionarios",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "Funcionarios",
                newName: "DataAdmissao");

            migrationBuilder.AlterColumn<string>(
                name: "Cargo",
                table: "Funcionarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Funcionarios",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NomeCompleto",
                table: "Funcionarios",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Setor",
                table: "Funcionarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Funcionarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Funcionarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funcionarios",
                table: "Funcionarios",
                column: "Id");
        }
    }
}
