using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DagnysBageriApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProductIdToOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PackSize",
                table: "Products",
                newName: "QuantityPerPack");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantityPerPack",
                table: "Products",
                newName: "PackSize");
        }
    }
}
