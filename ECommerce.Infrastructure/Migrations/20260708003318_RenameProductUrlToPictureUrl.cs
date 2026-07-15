using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameProductUrlToPictureUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_productsBrands_BrandID",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_productsTypes_TypeID",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "TypeID",
                table: "products",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "BrandID",
                table: "products",
                newName: "BrandId");

            migrationBuilder.RenameColumn(
                name: "ProductURL",
                table: "products",
                newName: "PictureUrl");

            migrationBuilder.RenameIndex(
                name: "IX_products_TypeID",
                table: "products",
                newName: "IX_products_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_products_BrandID",
                table: "products",
                newName: "IX_products_BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_productsBrands_BrandId",
                table: "products",
                column: "BrandId",
                principalTable: "productsBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_productsTypes_TypeId",
                table: "products",
                column: "TypeId",
                principalTable: "productsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_productsBrands_BrandId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_productsTypes_TypeId",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "products",
                newName: "TypeID");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "products",
                newName: "BrandID");

            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "products",
                newName: "ProductURL");

            migrationBuilder.RenameIndex(
                name: "IX_products_TypeId",
                table: "products",
                newName: "IX_products_TypeID");

            migrationBuilder.RenameIndex(
                name: "IX_products_BrandId",
                table: "products",
                newName: "IX_products_BrandID");

            migrationBuilder.AddForeignKey(
                name: "FK_products_productsBrands_BrandID",
                table: "products",
                column: "BrandID",
                principalTable: "productsBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_productsTypes_TypeID",
                table: "products",
                column: "TypeID",
                principalTable: "productsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
