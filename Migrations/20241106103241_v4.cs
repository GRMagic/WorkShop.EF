using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkShop.EF.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cursos",
                columns: ["Nome"],
                values: new object[,]
                {
                    { "Curso de Linq (C#)" }, 
                    { "Curso de Injeção de Dependencias (C#)" },
                });

            migrationBuilder.InsertData(
                table: "Estudantes",
                columns: ["Nome", "Sobrenome"],
                values: new object[,]
                {
                    { "Victor ", "Martins" },
                    { "Tomás ", "Fernandes" },
                    { "Manuela ", "Alves " },
                    { "Miguel ", "Rocha" },
                    { "Rodrigo", "Azevedo" },
                    { "Renan", "Ribeiro" },
                    { "Lara", "Carvalho" },
                    { "Nicolas", "Silva" },
                    { "Julian", "Cavalcanti" },
                    { "Luan", "Barbosa" },
                    { "Lorena", "Melo" },
                    { "Larissa", "Gomes" },
                    { "Vinícius", "Fernandes" },
                    { "Maria", "Sousa" },
                    { "Nicolas", "Melo" },
                    { "Leonardo", "Rocha" },
                    { "Júlia", "Pinto" },
                    { "Danilo", "Dias" },
                    { "Gabrielle", "Castro" },
                    { "Isabela", "Barbosa" },
                    { "Enzo", "Lima" },
                    { "Samuel", "Correia" },
                    { "Vitória", "Costa" },
                    { "Antônio", "Azevedo" },
                    { "Raissa", "Araujo" },
                    { "Eduardo", "Cunha" },
                    { "Rafaela", "Gomes" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
