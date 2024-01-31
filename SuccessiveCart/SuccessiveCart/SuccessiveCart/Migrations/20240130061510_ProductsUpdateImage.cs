using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuccessiveCart.Migrations
{
    /// <inheritdoc />
    public partial class ProductsUpdateImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_CartItems_CartItemCartId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CartItemCartId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CartItemCartId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "UserRole",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserPhoneNo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProductsProductId",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductsProductId",
                table: "CartItems",
                column: "ProductsProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductsProductId",
                table: "CartItems",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductsProductId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductsProductId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProductsProductId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CartItems");

            migrationBuilder.AlterColumn<string>(
                name: "UserRole",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UserPhoneNo",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CartItemCartId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CartItemCartId",
                table: "Products",
                column: "CartItemCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CartItems_CartItemCartId",
                table: "Products",
                column: "CartItemCartId",
                principalTable: "CartItems",
                principalColumn: "CartId");
        }
    }
}
