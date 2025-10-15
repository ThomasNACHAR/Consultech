using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Consultech.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMissionRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Clients_ClientId",
                table: "Missions");

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Consultants_ConsultantId",
                table: "Missions");

            migrationBuilder.AlterColumn<int>(
                name: "ConsultantId",
                table: "Missions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Missions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Clients_ClientId",
                table: "Missions",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Consultants_ConsultantId",
                table: "Missions",
                column: "ConsultantId",
                principalTable: "Consultants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Clients_ClientId",
                table: "Missions");

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Consultants_ConsultantId",
                table: "Missions");

            migrationBuilder.AlterColumn<int>(
                name: "ConsultantId",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Clients_ClientId",
                table: "Missions",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Consultants_ConsultantId",
                table: "Missions",
                column: "ConsultantId",
                principalTable: "Consultants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
