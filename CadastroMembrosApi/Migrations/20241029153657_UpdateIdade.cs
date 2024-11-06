using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadastroMembrosApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataDeNascimento",
                table: "Membros");

            migrationBuilder.AddColumn<int>(
                name: "Idade",
                table: "Membros",
                type: "int",
                maxLength: 80,
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Idade",
                table: "Membros");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeNascimento",
                table: "Membros",
                type: "datetime(6)",
                maxLength: 80,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
