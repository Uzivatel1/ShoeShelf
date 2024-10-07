using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesShelf.Migrations
{
    public partial class ldk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Defect",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoeID = table.Column<int>(type: "int", nullable: false),
                    Severity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defect", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Defect_Shoe_ShoeID",
                        column: x => x.ShoeID,
                        principalTable: "Shoe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Defect_ShoeID",
                table: "Defect",
                column: "ShoeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Defect");
        }
    }
}
