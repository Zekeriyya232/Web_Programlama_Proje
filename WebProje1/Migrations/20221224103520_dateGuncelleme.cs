using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProje1.Migrations
{
    public partial class dateGuncelleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "kullaniciDogum",
                table: "Kullanici",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "kullaniciDogum",
                table: "Kullanici",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");
        }
    }
}
