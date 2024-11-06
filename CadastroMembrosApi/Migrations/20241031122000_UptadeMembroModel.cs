using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadastroMembrosApi.Migrations
{
    /// <inheritdoc />
    public partial class UptadeMembroModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Idade",
                table: "Membros");

            migrationBuilder.AlterColumn<string>(
                name: "Cargo",
                table: "Membros",
                type: "varchar(80)",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeNascimento",
                table: "Membros",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Departamento",
                table: "Membros",
                type: "varchar(80)",
                maxLength: 80,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Membros",
                type: "varchar(300)",
                maxLength: 300,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataDeNascimento",
                table: "Membros");

            migrationBuilder.DropColumn(
                name: "Departamento",
                table: "Membros");

            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Membros");

            migrationBuilder.AlterColumn<string>(
                name: "Cargo",
                table: "Membros",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(80)",
                oldMaxLength: 80,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Idade",
                table: "Membros",
                type: "int",
                maxLength: 80,
                nullable: false,
                defaultValue: 0);
        }
    }
}
