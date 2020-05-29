using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BrunaPhotographSystem.InfraStructure.DataAccess.Migrations
{
    public partial class final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CPFNumero",
                table: "Clientes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CPF",
                columns: table => new
                {
                    Numero = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPF", x => x.Numero);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CPFNumero",
                table: "Clientes",
                column: "CPFNumero");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_CPF_CPFNumero",
                table: "Clientes",
                column: "CPFNumero",
                principalTable: "CPF",
                principalColumn: "Numero",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_CPF_CPFNumero",
                table: "Clientes");

            migrationBuilder.DropTable(
                name: "CPF");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_CPFNumero",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "CPFNumero",
                table: "Clientes");
        }
    }
}
