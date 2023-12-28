using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Address_Book.Repository.Data.Migrations
{
    public partial class AddingImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Entries");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Entries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entries_ImageId",
                table: "Entries",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Images_ImageId",
                table: "Entries",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Images_ImageId",
                table: "Entries");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Entries_ImageId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Entries");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Entries",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
