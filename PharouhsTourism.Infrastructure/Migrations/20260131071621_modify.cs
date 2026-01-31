using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharouhsTourism.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Destinations_DestinationId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_DestinationId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "Books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_DestinationId",
                table: "Books",
                column: "DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Destinations_DestinationId",
                table: "Books",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
