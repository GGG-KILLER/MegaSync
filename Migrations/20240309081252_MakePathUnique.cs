using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MegaSync.Migrations
{
    /// <inheritdoc />
    public partial class MakePathUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MegaLinks_Path",
                table: "MegaLinks",
                column: "Path",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MegaLinks_Path",
                table: "MegaLinks");
        }
    }
}
