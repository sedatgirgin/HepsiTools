using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HepsiTools.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionAnalyses_ConnectionInfo_ConnectionInfoId",
                table: "CompetitionAnalyses");

            migrationBuilder.DropTable(
                name: "CompetitionCompany");

            migrationBuilder.DropTable(
                name: "ConnectionInfo");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "UserLisans");

            migrationBuilder.DropIndex(
                name: "IX_CompetitionAnalyses_ConnectionInfoId",
                table: "CompetitionAnalyses");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Lisans",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Param",
                table: "CompetitionAnalyses",
                newName: "RepetitionCount");

            migrationBuilder.RenameColumn(
                name: "ConnectionInfoId",
                table: "CompetitionAnalyses",
                newName: "StatusType");

            migrationBuilder.RenameColumn(
                name: "SurName",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AspNetUsers",
                newName: "FirstName");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Lisans",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Lisans",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Lisans",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "CompetitionAnalyses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "CompetitionAnalyses",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "CompetitionAnalyses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParserLink",
                table: "CompetitionAnalyses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductInfo",
                table: "CompetitionAnalyses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductLink",
                table: "CompetitionAnalyses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SalePrice",
                table: "CompetitionAnalyses",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "CompetitionAnalyses",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyType = table.Column<int>(type: "integer", nullable: false),
                    SupplierId = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    CargoCompanyId = table.Column<string>(type: "text", nullable: true),
                    CustomResourceName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionAnalysesHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OldPrice = table.Column<double>(type: "double precision", nullable: true),
                    NewPrice = table.Column<double>(type: "double precision", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    HistoryType = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CompetitionAnalysesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionAnalysesHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionAnalysesHistory_CompetitionAnalyses_CompetitionA~",
                        column: x => x.CompetitionAnalysesId,
                        principalTable: "CompetitionAnalyses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lisans_UserId",
                table: "Lisans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionAnalyses_CompanyId",
                table: "CompetitionAnalyses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_UserId",
                table: "Company",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionAnalysesHistory_CompetitionAnalysesId",
                table: "CompetitionAnalysesHistory",
                column: "CompetitionAnalysesId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionAnalyses_Company_CompanyId",
                table: "CompetitionAnalyses",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lisans_AspNetUsers_UserId",
                table: "Lisans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionAnalyses_Company_CompanyId",
                table: "CompetitionAnalyses");

            migrationBuilder.DropForeignKey(
                name: "FK_Lisans_AspNetUsers_UserId",
                table: "Lisans");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "CompetitionAnalysesHistory");

            migrationBuilder.DropIndex(
                name: "IX_Lisans_UserId",
                table: "Lisans");

            migrationBuilder.DropIndex(
                name: "IX_CompetitionAnalyses_CompanyId",
                table: "CompetitionAnalyses");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Lisans");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Lisans");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Lisans");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CompetitionAnalyses");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "CompetitionAnalyses");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "CompetitionAnalyses");

            migrationBuilder.DropColumn(
                name: "ParserLink",
                table: "CompetitionAnalyses");

            migrationBuilder.DropColumn(
                name: "ProductInfo",
                table: "CompetitionAnalyses");

            migrationBuilder.DropColumn(
                name: "ProductLink",
                table: "CompetitionAnalyses");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "CompetitionAnalyses");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "CompetitionAnalyses");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Lisans",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StatusType",
                table: "CompetitionAnalyses",
                newName: "ConnectionInfoId");

            migrationBuilder.RenameColumn(
                name: "RepetitionCount",
                table: "CompetitionAnalyses",
                newName: "Param");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "SurName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "CompetitionCompany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionCompany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CargoCompanyId = table.Column<string>(type: "text", nullable: true),
                    CompanyType = table.Column<int>(type: "integer", nullable: false),
                    CustomResourceName = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    SupplierId = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConnectionInfo_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Note = table.Column<string>(type: "text", nullable: true),
                    WooCommerceDataId = table.Column<int>(type: "integer", nullable: false),
                    WooOrderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_WooCommerceData_WooCommerceDataId",
                        column: x => x.WooCommerceDataId,
                        principalTable: "WooCommerceData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Note = table.Column<string>(type: "text", nullable: true),
                    WooCommerceDataId = table.Column<int>(type: "integer", nullable: false),
                    WooProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_WooCommerceData_WooCommerceDataId",
                        column: x => x.WooCommerceDataId,
                        principalTable: "WooCommerceData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLisans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    LisansId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLisans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLisans_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLisans_Lisans_LisansId",
                        column: x => x.LisansId,
                        principalTable: "Lisans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionAnalyses_ConnectionInfoId",
                table: "CompetitionAnalyses",
                column: "ConnectionInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionInfo_UserId",
                table: "ConnectionInfo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_WooCommerceDataId",
                table: "Order",
                column: "WooCommerceDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_WooCommerceDataId",
                table: "Product",
                column: "WooCommerceDataId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLisans_LisansId",
                table: "UserLisans",
                column: "LisansId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLisans_UserId_LisansId_IsActive",
                table: "UserLisans",
                columns: new[] { "UserId", "LisansId", "IsActive" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionAnalyses_ConnectionInfo_ConnectionInfoId",
                table: "CompetitionAnalyses",
                column: "ConnectionInfoId",
                principalTable: "ConnectionInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
