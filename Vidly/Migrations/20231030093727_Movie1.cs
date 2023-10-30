using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vidly.Migrations
{
    /// <inheritdoc />
    public partial class Movie1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_MovieId",
                table: "Customers",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Movies_MovieId",
                table: "Customers",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Movies_MovieId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_MovieId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Customers");
        }
    }
}
