using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeinrealtions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShippingInfos");

            migrationBuilder.AddColumn<string>(
                name: "Shipping_Address",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Shipping_City",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Shipping_ContactName",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Shipping_Country",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Shipping_Courier",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Shipping_Phone",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Shipping_State",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Shipping_ZipCode",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shipping_Address",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Shipping_City",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Shipping_ContactName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Shipping_Country",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Shipping_Courier",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Shipping_Phone",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Shipping_State",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Shipping_ZipCode",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "ShippingInfos",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    ContactName = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    Courier = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    ZipCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingInfos", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_ShippingInfos_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
