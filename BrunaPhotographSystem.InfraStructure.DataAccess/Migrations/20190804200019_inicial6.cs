using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BrunaPhotographSystem.InfraStructure.DataAccess.Migrations
{
    public partial class inicial6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
           

            migrationBuilder.AddColumn<Guid>(
                name: "ClienteId",
                table: "Albuns",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Albuns_ClienteId",
                table: "Albuns",
                column: "ClienteId");

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

            migrationBuilder.DropIndex(
                name: "IX_Albuns_ClienteId",
                table: "Albuns");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Albuns");

            migrationBuilder.RenameColumn(
                name: "clienteId",
                table: "Albuns",
                newName: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Albuns_ClienteId",
                table: "Albuns",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albuns_Clientes_ClienteId",
                table: "Albuns",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
