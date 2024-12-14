using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DagnysBageriApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedNavigationpropertiestoclasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SupplierMaterials_RawMaterialId",
                table: "SupplierMaterials",
                column: "RawMaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierMaterials_RawMaterials_RawMaterialId",
                table: "SupplierMaterials",
                column: "RawMaterialId",
                principalTable: "RawMaterials",
                principalColumn: "RawMaterialId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierMaterials_Suppliers_SupplierId",
                table: "SupplierMaterials",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierMaterials_RawMaterials_RawMaterialId",
                table: "SupplierMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierMaterials_Suppliers_SupplierId",
                table: "SupplierMaterials");

            migrationBuilder.DropIndex(
                name: "IX_SupplierMaterials_RawMaterialId",
                table: "SupplierMaterials");
        }
    }
}
