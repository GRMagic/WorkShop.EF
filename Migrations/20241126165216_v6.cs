using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkShop.EF.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CursoProfessor",
                columns: table => new
                {
                    CursoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ProfessorId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoProfessor", x => new { x.CursoId, x.ProfessorId });
                    table.ForeignKey(
                        name: "FK_CursoProfessor_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoProfessor_Professores_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CursoProfessor_ProfessorId",
                table: "CursoProfessor",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Professores_Nome",
                table: "Professores",
                column: "Nome",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CursoProfessor");

            migrationBuilder.DropTable(
                name: "Professores");
        }
    }
}
