using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCI.MamaGuide.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HospitalId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                table: "Articles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "VerifiedArticle",
                columns: table => new
                {
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerifiedArticle", x => new { x.ArticleId, x.AdminId });
                    table.ForeignKey(
                        name: "FK_VerifiedArticle_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VerifiedArticle_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_HospitalId",
                table: "Doctors",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_DoctorId",
                table: "Articles",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_VerifiedArticle_AdminId",
                table: "VerifiedArticle",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Doctors_DoctorId",
                table: "Articles",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Hospitals_HospitalId",
                table: "Doctors",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Doctors_DoctorId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Hospitals_HospitalId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "VerifiedArticle");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_HospitalId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Articles_DoctorId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Articles");
        }
    }
}
