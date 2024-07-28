using Basis.Biblioteca.Domain.DomainObjects;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Basis.Biblioteca.Infrastructure.Persistence.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class IniticalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assunto",
                columns: table => new
                {
                    CodAs = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assunto", x => x.CodAs);
                });

            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    CodAu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.CodAu);
                });

            migrationBuilder.CreateTable(
                name: "Livro",
                columns: table => new
                {
                    CodL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Editora = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Edicao = table.Column<int>(type: "int", nullable: false),
                    AnoPublicacao = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro", x => x.CodL);
                });

            migrationBuilder.CreateTable(
                name: "Livro_Assunto",
                columns: table => new
                {
                    CodAs = table.Column<int>(type: "int", nullable: false),
                    CodL = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro_Assunto", x => new { x.CodAs, x.CodL });
                    table.ForeignKey(
                        name: "FK_Livro_Assunto_Assunto_CodAs",
                        column: x => x.CodAs,
                        principalTable: "Assunto",
                        principalColumn: "CodAs",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livro_Assunto_Livro_CodL",
                        column: x => x.CodL,
                        principalTable: "Livro",
                        principalColumn: "CodL",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Livro_Autor",
                columns: table => new
                {
                    CodAu = table.Column<int>(type: "int", nullable: false),
                    CodL = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro_Autor", x => new { x.CodAu, x.CodL });
                    table.ForeignKey(
                        name: "FK_Livro_Autor_Autor_CodAu",
                        column: x => x.CodAu,
                        principalTable: "Autor",
                        principalColumn: "CodAu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livro_Autor_Livro_CodL",
                        column: x => x.CodL,
                        principalTable: "Livro",
                        principalColumn: "CodL",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrecoVenda",
                columns: table => new
                {
                    CodP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDeVenda = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    CodL = table.Column<int>(type: "int", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrecoVenda", x => x.CodP);
                    table.ForeignKey(
                        name: "FK_PrecoVenda_Livro_CodL",
                        column: x => x.CodL,
                        principalTable: "Livro",
                        principalColumn: "CodL");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livro_Assunto_CodL",
                table: "Livro_Assunto",
                column: "CodL");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_Autor_CodL",
                table: "Livro_Autor",
                column: "CodL");

            migrationBuilder.CreateIndex(
                name: "IX_PrecoVenda_CodL",
                table: "PrecoVenda",
                column: "CodL");

            SeedData(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livro_Assunto");

            migrationBuilder.DropTable(
                name: "Livro_Autor");

            migrationBuilder.DropTable(
                name: "PrecoVenda");

            migrationBuilder.DropTable(
                name: "Assunto");

            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "Livro");
        }

        private void SeedData(MigrationBuilder migrationBuilder)
        {
            // Seed data for Autores
            migrationBuilder.InsertData(
                table: "Autor",
                columns: new[] { "CodAu", "Nome", "DataCriacao" },
                values: new object[,]
                {
                { 1, "Colleen Hoover", DateTime.UtcNow },
                { 2, "Matt Haig", DateTime.UtcNow },
                { 3, "Missionários Redentoristas", DateTime.UtcNow },
                { 4, "Junior Rostirola", DateTime.UtcNow },
                { 5, "Leo Chaves", DateTime.UtcNow },
                { 6, "Vivian Dias", DateTime.UtcNow },
                { 7, "Maurício Dias", DateTime.UtcNow },
                { 8, "Charles Duhigg", DateTime.UtcNow },
                { 9, "T. Harv Eker", DateTime.UtcNow },
                { 10, "Mark Manson", DateTime.UtcNow },
                { 11, "Hal Elrod", DateTime.UtcNow },
                { 12, "Carol S. Dweck", DateTime.UtcNow },
                { 13, "George S. Clason", DateTime.UtcNow },
                { 14, "Robert T. Kiyosaki", DateTime.UtcNow },
                { 15, "Sharon Lechter", DateTime.UtcNow },
                { 16, "Antoine de Saint-Exupéry", DateTime.UtcNow },
                { 17, "George Orwell", DateTime.UtcNow },
                { 18, "Paulo Coelho", DateTime.UtcNow },
                { 19, "Yuval Noah Harari", DateTime.UtcNow },
                { 20, "Brené Brown", DateTime.UtcNow },
                { 21, "Taylor Jenkins Reid", DateTime.UtcNow },
                { 22, "Itamar Vieira Junior", DateTime.UtcNow },
                { 23, "Margaret Atwood", DateTime.UtcNow },
                { 24, "Paula Hawkins", DateTime.UtcNow },
                { 25, "Markus Zusak", DateTime.UtcNow },
                { 26, "Dan Brown", DateTime.UtcNow },
                { 27, "Patrick Rothfuss", DateTime.UtcNow }
                });

            // Seed data for Assuntos
            migrationBuilder.InsertData(
                table: "Assunto",
                columns: new[] { "CodAs", "Descricao", "DataCriacao" },
                values: new object[,]
                {
                { 1, "Romance", DateTime.UtcNow },
                { 2, "Ficção", DateTime.UtcNow },
                { 3, "Religioso", DateTime.UtcNow },
                { 4, "Autoajuda", DateTime.UtcNow },
                { 5, "Educação", DateTime.UtcNow },
                { 6, "Clássico", DateTime.UtcNow },
                { 7, "História", DateTime.UtcNow },
                { 8, "Filosofia", DateTime.UtcNow }
                });

            // Seed data for Livros
            migrationBuilder.InsertData(
                table: "Livro",
                columns: new[] { "CodL", "Titulo", "Editora", "Edicao", "AnoPublicacao", "DataCriacao" },
                values: new object[,]
                {
                { 1, "É Assim que Acaba", "Galera Record", 1, "2024", DateTime.UtcNow },
                { 2, "A Biblioteca da Meia-Noite", "Bertrand Brasil", 1, "2024", DateTime.UtcNow },
                { 3, "Novena e Festa da Padroeira do Brasil", "Santuário", 1, "2024", DateTime.UtcNow },
                { 4, "Café com Deus Pai", "Vélos", 1, "2024", DateTime.UtcNow },
                { 5, "Mundo Mais Consciente", "Novo Século", 1, "2024", DateTime.UtcNow }
                });

            // Relacionar Livros com Autores
            migrationBuilder.InsertData(
                table: "Livro_Autor",
                columns: new[] { "CodL", "CodAu" },
                values: new object[,]
                {
                { 1, 1 },
                { 2, 2 },
                { 3, 3 },
                { 4, 4 },
                { 5, 5 },
                { 5, 6 },
                { 5, 7 }
                });

            // Relacionar Livros com Assuntos
            migrationBuilder.InsertData(
                table: "Livro_Assunto",
                columns: new[] { "CodL", "CodAs" },
                values: new object[,]
                {
                { 1, 1 },
                { 2, 2 },
                { 3, 3 },
                { 4, 3 },
                { 5, 5 }
                });

            // Seed data for PrecoVenda
            migrationBuilder.InsertData(
                table: "PrecoVenda",
                columns: new[] { "CodP", "CodL", "TipoDeVenda", "Preco", "DataCriacao" },
                values: new object[,]
                {
                { 1, 1, (int)TipoDeVendaType.Internet, 39.90m, DateTime.UtcNow },
                { 2, 2, (int)TipoDeVendaType.Internet, 29.90m, DateTime.UtcNow },
                { 3, 3, (int)TipoDeVendaType.Internet, 19.90m, DateTime.UtcNow },
                { 4, 4, (int)TipoDeVendaType.Internet, 49.90m, DateTime.UtcNow },
                { 5, 5, (int)TipoDeVendaType.Internet, 59.90m, DateTime.UtcNow }
                });
        }
    }
}
