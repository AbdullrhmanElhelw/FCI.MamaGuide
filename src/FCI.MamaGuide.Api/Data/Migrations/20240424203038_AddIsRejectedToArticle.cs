using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCI.MamaGuide.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsRejectedToArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "Articles");
        }
    }
}
