using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Consultech.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "startDate",
                table: "Consultants",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Consultants",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "isAvailable",
                table: "Consultants",
                newName: "IsAvailable");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Consultants",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Consultants",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Consultants",
                newName: "startDate");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Consultants",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "Consultants",
                newName: "isAvailable");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Consultants",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Consultants",
                newName: "email");
        }
    }
}
