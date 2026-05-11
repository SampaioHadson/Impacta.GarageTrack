using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Impacta.GarageTrack.System.Api.Migrations
{
    /// <inheritdoc />
    public partial class ParkingTaxAfterHourTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FromHours",
                table: "ParkingTaxes",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromHours",
                table: "ParkingTaxes");
        }
    }
}
