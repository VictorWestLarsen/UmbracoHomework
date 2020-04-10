using Microsoft.EntityFrameworkCore.Migrations;

namespace UmbracoRaffle.Migrations
{
    public partial class error : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Age",
                table: "Raffle",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Age",
                table: "Raffle",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
