using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace K8S.DriverAPI.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedDriverModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Driver",
                table: "Achievements");

            migrationBuilder.DropIndex(
                name: "IX_Achievements_DriverId",
                table: "Achievements");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_DriverId",
                table: "Achievements",
                column: "DriverId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Drivers_DriverId",
                table: "Achievements",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Drivers_DriverId",
                table: "Achievements");

            migrationBuilder.DropIndex(
                name: "IX_Achievements_DriverId",
                table: "Achievements");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_DriverId",
                table: "Achievements",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Driver",
                table: "Achievements",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");
        }
    }
}
