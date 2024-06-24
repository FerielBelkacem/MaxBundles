using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaxBundles.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductType = table.Column<int>(type: "int", nullable: false),
                    InventoryCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Bundles",
                columns: table => new
                {
                    BundleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bundles", x => x.BundleId);
                    table.ForeignKey(
                        name: "FK_Bundles_Products_BundleId",
                        column: x => x.BundleId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BundleParts",
                columns: table => new
                {
                    BundlePartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BundleId = table.Column<int>(type: "int", nullable: false),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BundleParts", x => x.BundlePartId);
                    table.ForeignKey(
                        name: "FK_BundleParts_Bundles_BundleId",
                        column: x => x.BundleId,
                        principalTable: "Bundles",
                        principalColumn: "BundleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BundleParts_Products_PartId",
                        column: x => x.PartId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BundleParts_BundleId",
                table: "BundleParts",
                column: "BundleId");

            migrationBuilder.CreateIndex(
                name: "IX_BundleParts_PartId",
                table: "BundleParts",
                column: "PartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BundleParts");

            migrationBuilder.DropTable(
                name: "Bundles");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
