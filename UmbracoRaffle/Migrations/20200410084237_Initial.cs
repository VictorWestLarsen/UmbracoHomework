using Microsoft.EntityFrameworkCore.Migrations;

namespace UmbracoRaffle.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Raffle",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Firstname = table.Column<string>(maxLength: 60, nullable: true),
                    Lastname = table.Column<string>(maxLength: 60, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Age = table.Column<string>(nullable: false),
                    Serialnumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raffle", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Serialnumbers",
                columns: table => new
                {
                    Number = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serialnumbers", x => x.Number);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Raffle");

            migrationBuilder.DropTable(
                name: "Serialnumbers");
        }
    }
}
