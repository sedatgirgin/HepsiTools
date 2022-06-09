using Microsoft.EntityFrameworkCore.Migrations;

namespace HepsiTools.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Multiple",
                table: "CompetitionAnalyses",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Multiple",
                table: "CompetitionAnalyses",
                type: "text",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
