using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIAgendamento.Migrations
{
    public partial class AtualizaPacienteMedico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Pacientes_PacienteId",
                table: "Agendamentos");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Agendamentos_PacienteId",
                table: "Agendamentos");

            migrationBuilder.RenameColumn(
                name: "PacienteId",
                table: "Agendamentos",
                newName: "IdPaciente");

            migrationBuilder.AlterColumn<string>(
                name: "Motivo",
                table: "Agendamentos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "IdMedico",
                table: "Agendamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdMedico",
                table: "Agendamentos");

            migrationBuilder.RenameColumn(
                name: "IdPaciente",
                table: "Agendamentos",
                newName: "PacienteId");

            migrationBuilder.UpdateData(
                table: "Agendamentos",
                keyColumn: "Motivo",
                keyValue: null,
                column: "Motivo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Motivo",
                table: "Agendamentos",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    IdPaciente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataNascimento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sobrenome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.IdPaciente);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_PacienteId",
                table: "Agendamentos",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Pacientes_PacienteId",
                table: "Agendamentos",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "IdPaciente",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
