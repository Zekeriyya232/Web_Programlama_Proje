using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProje1.Migrations
{
    public partial class imgTablo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilImg",
                table: "Kullanici",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "no_image.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilImg",
                table: "Kullanici");
        }
    }
}
