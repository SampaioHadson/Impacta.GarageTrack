using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Impacta.GarageTrack.System.Api.Migrations
{
    /// <inheritdoc />
    public partial class UserFinishedParking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parkings_Users_FinishedByUserId",
                table: "Parkings");

            migrationBuilder.AlterColumn<long>(
                name: "FinishedByUserId",
                table: "Parkings",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Parkings_Users_FinishedByUserId",
                table: "Parkings",
                column: "FinishedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parkings_Users_FinishedByUserId",
                table: "Parkings");

            migrationBuilder.AlterColumn<long>(
                name: "FinishedByUserId",
                table: "Parkings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Parkings_Users_FinishedByUserId",
                table: "Parkings",
                column: "FinishedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
