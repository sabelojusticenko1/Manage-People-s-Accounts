using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagePeople.Migrations
{
    public partial class Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbPerson",
                columns: table => new
                {
                    IdPerson = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: false),
                    Age = table.Column<string>(nullable: false),
                    IDNumber = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbPerson", x => x.IdPerson);
                });

            migrationBuilder.CreateTable(
                name: "TbAccounts",
                columns: table => new
                {
                    IdAccount = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPerson = table.Column<int>(nullable: true),
                    AccountNo = table.Column<string>(nullable: false),
                    AccountHolder = table.Column<string>(nullable: false),
                    AccountName = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbAccounts", x => x.IdAccount);
                    table.ForeignKey(
                        name: "FK_TbAccounts_TbPerson_IdPerson",
                        column: x => x.IdPerson,
                        principalTable: "TbPerson",
                        principalColumn: "IdPerson",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TbTransations",
                columns: table => new
                {
                    TransationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAccount = table.Column<int>(nullable: true),
                    TransationDate = table.Column<DateTime>(nullable: false),
                    AccountHolder = table.Column<string>(maxLength: 15, nullable: false),
                    TransationDetails = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbTransations", x => x.TransationId);
                    table.ForeignKey(
                        name: "FK_TbTransations_TbAccounts_IdAccount",
                        column: x => x.IdAccount,
                        principalTable: "TbAccounts",
                        principalColumn: "IdAccount",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbAccounts_IdPerson",
                table: "TbAccounts",
                column: "IdPerson");

            migrationBuilder.CreateIndex(
                name: "IX_TbTransations_IdAccount",
                table: "TbTransations",
                column: "IdAccount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbTransations");

            migrationBuilder.DropTable(
                name: "TbAccounts");

            migrationBuilder.DropTable(
                name: "TbPerson");
        }
    }
}
