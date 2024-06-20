using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCI.MamaGuide.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProfilePictureModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProfilePictureId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProfilePicture",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePicture", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_ProfilePictureId",
                table: "Doctors",
                column: "ProfilePictureId",
                unique: true,
                filter: "[ProfilePictureId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_ProfilePicture_ProfilePictureId",
                table: "Doctors",
                column: "ProfilePictureId",
                principalTable: "ProfilePicture",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_ProfilePicture_ProfilePictureId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "ProfilePicture");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_ProfilePictureId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "Doctors");
        }
    }
}
