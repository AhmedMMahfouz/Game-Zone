using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameHub.Migrations
{
    /// <inheritdoc />
    public partial class change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Catogrys_CatogryId",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Catogrys",
                table: "Catogrys");

            migrationBuilder.RenameTable(
                name: "Catogrys",
                newName: "Catogries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Catogries",
                table: "Catogries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Catogries_CatogryId",
                table: "Games",
                column: "CatogryId",
                principalTable: "Catogries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Catogries_CatogryId",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Catogries",
                table: "Catogries");

            migrationBuilder.RenameTable(
                name: "Catogries",
                newName: "Catogrys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Catogrys",
                table: "Catogrys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Catogrys_CatogryId",
                table: "Games",
                column: "CatogryId",
                principalTable: "Catogrys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
