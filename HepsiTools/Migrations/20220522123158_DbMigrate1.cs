using Microsoft.EntityFrameworkCore.Migrations;

namespace HepsiTools.Migrations
{
    public partial class DbMigrate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Companie",
                table: "Companie");

            migrationBuilder.RenameTable(
                name: "Companie",
                newName: "CompetitionCompany");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompetitionCompany",
                table: "CompetitionCompany",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompetitionCompany",
                table: "CompetitionCompany");

            migrationBuilder.RenameTable(
                name: "CompetitionCompany",
                newName: "Companie");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companie",
                table: "Companie",
                column: "Id");
        }
    }
}
