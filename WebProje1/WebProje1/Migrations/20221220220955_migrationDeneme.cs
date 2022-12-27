using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProje1.Migrations
{
    public partial class migrationDeneme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "kullaniciDogum",
                table: "Kullanici",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "kullaniciDogum",
                table: "Kullanici");
        }
    }
}
