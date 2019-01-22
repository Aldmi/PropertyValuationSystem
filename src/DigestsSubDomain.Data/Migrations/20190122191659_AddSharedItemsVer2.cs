using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Digests.Data.EfCore.Migrations
{
    public partial class AddSharedItemsVer2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_WallMaterials_WallMaterialId",
                table: "Houses");

            migrationBuilder.DropForeignKey(
                name: "FK_WallMaterials_SharedItems_EfSharedItemsId",
                table: "WallMaterials");

            migrationBuilder.DropTable(
                name: "SharedItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WallMaterials",
                table: "WallMaterials");

            migrationBuilder.DropIndex(
                name: "IX_WallMaterials_EfSharedItemsId",
                table: "WallMaterials");

            migrationBuilder.DropColumn(
                name: "EfSharedItemsId",
                table: "WallMaterials");

            migrationBuilder.RenameTable(
                name: "WallMaterials",
                newName: "EfWallMaterial");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EfWallMaterial",
                table: "EfWallMaterial",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_EfWallMaterial_WallMaterialId",
                table: "Houses",
                column: "WallMaterialId",
                principalTable: "EfWallMaterial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_EfWallMaterial_WallMaterialId",
                table: "Houses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EfWallMaterial",
                table: "EfWallMaterial");

            migrationBuilder.RenameTable(
                name: "EfWallMaterial",
                newName: "WallMaterials");

            migrationBuilder.AddColumn<int>(
                name: "EfSharedItemsId",
                table: "WallMaterials",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WallMaterials",
                table: "WallMaterials",
                column: "Id");

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
                name: "FK_Houses_WallMaterials_WallMaterialId",
                table: "Houses",
                column: "WallMaterialId",
                principalTable: "WallMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WallMaterials_SharedItems_EfSharedItemsId",
                table: "WallMaterials",
                column: "EfSharedItemsId",
                principalTable: "SharedItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
