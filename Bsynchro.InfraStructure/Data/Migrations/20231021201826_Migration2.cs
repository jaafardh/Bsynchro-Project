using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bsynchro.InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InitialCredit",
                table: "Customer",
                newName: "Balance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "Customer",
                newName: "InitialCredit");
        }
    }
}
