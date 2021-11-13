using Microsoft.EntityFrameworkCore.Migrations;

namespace PokemonWebApi_Auth0.Migrations
{
    public partial class InitialMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonItems_Pokedexes_PokedexId",
                table: "PokemonItems");

            migrationBuilder.DropColumn(
                name: "PokemonBreed",
                table: "PokemonItems");

            migrationBuilder.AlterColumn<int>(
                name: "PokedexId",
                table: "PokemonItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonItems_Pokedexes_PokedexId",
                table: "PokemonItems",
                column: "PokedexId",
                principalTable: "Pokedexes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonItems_Pokedexes_PokedexId",
                table: "PokemonItems");

            migrationBuilder.AlterColumn<int>(
                name: "PokedexId",
                table: "PokemonItems",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "PokemonBreed",
                table: "PokemonItems",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonItems_Pokedexes_PokedexId",
                table: "PokemonItems",
                column: "PokedexId",
                principalTable: "Pokedexes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
