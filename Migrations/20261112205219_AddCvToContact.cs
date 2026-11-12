using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MYAcademy_PortfolioProject.Migrations
{
    /// <inheritdoc />
    public partial class AddCvToContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CvFilePath",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CvFilePath",
                table: "Contacts");
        }
    }
}
