using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.src.Migrations
{
    /// <inheritdoc />
    public partial class AddBuildingUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Buildings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_UserId",
                table: "Buildings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Users_UserId",
                table: "Buildings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Users_UserId",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_UserId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Buildings");
        }
    }
}
