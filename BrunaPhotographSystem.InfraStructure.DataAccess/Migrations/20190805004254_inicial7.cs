using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BrunaPhotographSystem.InfraStructure.DataAccess.Migrations
{
    public partial class inicial7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
            migrationBuilder.AddColumn<Guid>(
               name: "clienteId",
               table: "Albuns",
               nullable: true);
            migrationBuilder.CreateIndex(
                name: "IX_Albuns_clienteId",
                table: "Albuns",
                column: "clienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albuns_Clientes_clienteId",
                table: "Albuns",
                column: "clienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albuns_Clientes_clienteId",
                table: "Albuns");

            migrationBuilder.DropIndex(
                name: "IX_Albuns_clienteId",
                table: "Albuns");
            migrationBuilder.DropColumn(
                name: "clienteId",
                table: "Albuns");
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
    }
}
