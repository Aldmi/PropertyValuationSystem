using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Digests.Data.EfCore.Migrations
{
    public partial class AddSharedItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EfSharedItemsId",
                table: "WallMaterials",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SharedItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedItems", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WallMaterials_EfSharedItemsId",
                table: "WallMaterials",
                column: "EfSharedItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_WallMaterials_SharedItems_EfSharedItemsId",
                table: "WallMaterials",
                column: "EfSharedItemsId",
                principalTable: "SharedItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WallMaterials_SharedItems_EfSharedItemsId",
                table: "WallMaterials");

            migrationBuilder.DropTable(
                name: "SharedItems");

            migrationBuilder.DropIndex(
                name: "IX_WallMaterials_EfSharedItemsId",
                table: "WallMaterials");

            migrationBuilder.DropColumn(
                name: "EfSharedItemsId",
                table: "WallMaterials");
        }
    }
}
