using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DagnysBageriApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class rawMaterialsController : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "RawMaterials");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "RawMaterials",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
