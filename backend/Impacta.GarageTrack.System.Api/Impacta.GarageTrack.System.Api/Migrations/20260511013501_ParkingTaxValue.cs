using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Impacta.GarageTrack.System.Api.Migrations
{
    /// <inheritdoc />
    public partial class ParkingTaxValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalValue",
                table: "Parkings",
                type: "numeric(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalValue",
                table: "Parkings");
        }
    }
}
