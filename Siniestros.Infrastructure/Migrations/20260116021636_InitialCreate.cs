using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Siniestros.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nchar(2)", fixedLength: true, maxLength: 2, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoSiniestro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoSiniestro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ciudad",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nchar(5)", fixedLength: true, maxLength: 5, nullable: false),
                    IdDepartamento = table.Column<string>(type: "nchar(2)", fixedLength: true, maxLength: 2, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ciudad_Departamento_IdDepartamento",
                        column: x => x.IdDepartamento,
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Siniestros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCiudad = table.Column<string>(type: "nchar(5)", fixedLength: true, maxLength: 5, nullable: false),
                    IdTipoSiniestro = table.Column<int>(type: "int", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehiculosInvolucrados = table.Column<int>(type: "int", nullable: false),
                    NumeroVictimas = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siniestros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Siniestros_Ciudad_IdCiudad",
                        column: x => x.IdCiudad,
                        principalTable: "Ciudad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Siniestros_TipoSiniestro_IdTipoSiniestro",
                        column: x => x.IdTipoSiniestro,
                        principalTable: "TipoSiniestro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ciudad_IdDepartamento_Nombre",
                table: "Ciudad",
                columns: new[] { "IdDepartamento", "Nombre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departamento_Nombre",
                table: "Departamento",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Siniestros_FechaHora",
                table: "Siniestros",
                column: "FechaHora");

            migrationBuilder.CreateIndex(
                name: "IX_Siniestros_IdCiudad",
                table: "Siniestros",
                column: "IdCiudad");

            migrationBuilder.CreateIndex(
                name: "IX_Siniestros_IdTipoSiniestro",
                table: "Siniestros",
                column: "IdTipoSiniestro");

            migrationBuilder.CreateIndex(
                name: "IX_TipoSiniestro_Nombre",
                table: "TipoSiniestro",
                column: "Nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Siniestros");

            migrationBuilder.DropTable(
                name: "Ciudad");

            migrationBuilder.DropTable(
                name: "TipoSiniestro");

            migrationBuilder.DropTable(
                name: "Departamento");
        }
    }
}
