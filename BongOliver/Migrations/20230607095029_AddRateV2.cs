using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BongOliver.Migrations
{
    /// <inheritdoc />
    public partial class AddRateV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rate_Services_serviceId",
                table: "Rate");

            migrationBuilder.DropForeignKey(
                name: "FK_Rate_Users_userId",
                table: "Rate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rate",
                table: "Rate");

            migrationBuilder.RenameTable(
                name: "Rate",
                newName: "Rates");

            migrationBuilder.RenameIndex(
                name: "IX_Rate_userId",
                table: "Rates",
                newName: "IX_Rates_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Rate_serviceId",
                table: "Rates",
                newName: "IX_Rates_serviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rates",
                table: "Rates",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Services_serviceId",
                table: "Rates",
                column: "serviceId",
                principalTable: "Services",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Users_userId",
                table: "Rates",
                column: "userId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Services_serviceId",
                table: "Rates");

            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Users_userId",
                table: "Rates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rates",
                table: "Rates");

            migrationBuilder.RenameTable(
                name: "Rates",
                newName: "Rate");

            migrationBuilder.RenameIndex(
                name: "IX_Rates_userId",
                table: "Rate",
                newName: "IX_Rate_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Rates_serviceId",
                table: "Rate",
                newName: "IX_Rate_serviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rate",
                table: "Rate",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rate_Services_serviceId",
                table: "Rate",
                column: "serviceId",
                principalTable: "Services",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rate_Users_userId",
                table: "Rate",
                column: "userId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
