using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaterialUI.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data1 = table.Column<string>(type: "TEXT", nullable: false),
                    Data2 = table.Column<string>(type: "TEXT", nullable: false),
                    Data3 = table.Column<string>(type: "TEXT", nullable: false),
                    Data4 = table.Column<string>(type: "TEXT", nullable: false),
                    Data5 = table.Column<string>(type: "TEXT", nullable: false),
                    Data6 = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Data", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Data");
        }
    }
}
