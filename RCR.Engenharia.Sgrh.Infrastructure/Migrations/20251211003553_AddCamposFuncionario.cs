using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RCR.Engenharia.Sgrh.Infrastructure.Migrations
{
    public partial class AddCamposFuncionario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "rg",
                schema: "dbo",
                table: "funcionarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "cpf",
                schema: "dbo",
                table: "funcionarios",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "celular",
                schema: "dbo",
                table: "funcionarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "email",
                schema: "dbo",
                table: "funcionarios",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "data_admissao",
                schema: "dbo",
                table: "funcionarios",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "salario",
                schema: "dbo",
                table: "funcionarios",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "foto",
                schema: "dbo",
                table: "funcionarios",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rg",
                schema: "dbo",
                table: "funcionarios");

            migrationBuilder.DropColumn(
                name: "cpf",
                schema: "dbo",
                table: "funcionarios");

            migrationBuilder.DropColumn(
                name: "celular",
                schema: "dbo",
                table: "funcionarios");

            migrationBuilder.DropColumn(
                name: "email",
                schema: "dbo",
                table: "funcionarios");

            migrationBuilder.DropColumn(
                name: "data_admissao",
                schema: "dbo",
                table: "funcionarios");

            migrationBuilder.DropColumn(
                name: "salario",
                schema: "dbo",
                table: "funcionarios");

            migrationBuilder.DropColumn(
                name: "foto",
                schema: "dbo",
                table: "funcionarios");
        }
    }
}
