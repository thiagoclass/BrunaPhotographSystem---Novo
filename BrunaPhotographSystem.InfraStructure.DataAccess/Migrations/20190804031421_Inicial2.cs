using Microsoft.EntityFrameworkCore.Migrations;

namespace BrunaPhotographSystem.InfraStructure.DataAccess.Migrations
{
    public partial class Inicial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_Clientes_ClienteId",
                table: "Album");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Album",
                table: "Album");

            migrationBuilder.RenameTable(
                name: "Album",
                newName: "Albuns");

            migrationBuilder.RenameIndex(
                name: "IX_Album_ClienteId",
                table: "Albuns",
                newName: "IX_Albuns_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Albuns",
                table: "Albuns",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Albuns_Clientes_ClienteId",
                table: "Albuns",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albuns_Clientes_ClienteId",
                table: "Albuns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Albuns",
                table: "Albuns");

            migrationBuilder.RenameTable(
                name: "Albuns",
                newName: "Album");

            migrationBuilder.RenameIndex(
                name: "IX_Albuns_ClienteId",
                table: "Album",
                newName: "IX_Album_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Album",
                table: "Album",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Clientes_ClienteId",
                table: "Album",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
