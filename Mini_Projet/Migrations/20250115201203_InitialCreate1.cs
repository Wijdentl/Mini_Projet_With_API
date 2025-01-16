using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mini_Projet.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Reclamations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reclamations_ArticleId",
                table: "Reclamations",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reclamations_Articles_ArticleId",
                table: "Reclamations",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reclamations_Articles_ArticleId",
                table: "Reclamations");

            migrationBuilder.DropIndex(
                name: "IX_Reclamations_ArticleId",
                table: "Reclamations");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Reclamations");
        }
    }
}
