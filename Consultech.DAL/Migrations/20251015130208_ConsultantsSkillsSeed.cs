using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Consultech.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ConsultantsSkillsSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ConsultantSkill",
                columns: new[] { "ConsultantsId", "SkillsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 4 },
                    { 2, 2 },
                    { 2, 5 },
                    { 3, 1 },
                    { 3, 3 },
                    { 4, 3 },
                    { 5, 4 },
                    { 5, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConsultantSkill",
                keyColumns: new[] { "ConsultantsId", "SkillsId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ConsultantSkill",
                keyColumns: new[] { "ConsultantsId", "SkillsId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "ConsultantSkill",
                keyColumns: new[] { "ConsultantsId", "SkillsId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ConsultantSkill",
                keyColumns: new[] { "ConsultantsId", "SkillsId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "ConsultantSkill",
                keyColumns: new[] { "ConsultantsId", "SkillsId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "ConsultantSkill",
                keyColumns: new[] { "ConsultantsId", "SkillsId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "ConsultantSkill",
                keyColumns: new[] { "ConsultantsId", "SkillsId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "ConsultantSkill",
                keyColumns: new[] { "ConsultantsId", "SkillsId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "ConsultantSkill",
                keyColumns: new[] { "ConsultantsId", "SkillsId" },
                keyValues: new object[] { 5, 5 });
        }
    }
}
