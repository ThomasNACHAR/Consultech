using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Consultech.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "ActivitySector", "Address", "CompanyName", "Email" },
                values: new object[,]
                {
                    { 1, 4, "1007 Mountain Drive, Gotham", "Wayne Enterprises", "contact@wayneenterprises.com" },
                    { 2, 3, "200 Park Avenue, New York", "Stark Industries", "info@starkindustries.com" },
                    { 3, 8, "1 Planet Plaza, Metropolis", "Daily Planet", "editor@dailyplanet.com" },
                    { 4, 0, "Norman Tower, New York", "Oscorp Technologies", "admin@oscorp.com" },
                    { 5, 3, "Star City Central Ave", "Queen Consolidated", "hq@queenconsolidated.com" }
                });

            migrationBuilder.InsertData(
                table: "Consultants",
                columns: new[] { "Id", "Email", "FirstName", "IsAvailable", "LastName", "StartDate" },
                values: new object[,]
                {
                    { 1, "bruce@wayneenterprises.com", "Bruce", true, "Wayne", new DateTime(2010, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "tony@starkindustries.com", "Tony", false, "Stark", new DateTime(2012, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "diana@themiscira.org", "Diana", true, "Prince", new DateTime(2015, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "clark@dailyplanet.com", "Clark", false, "Kent", new DateTime(2011, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "peter@dailybugle.com", "Peter", true, "Parker", new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Category", "Level", "Title" },
                values: new object[,]
                {
                    { 1, "Soft Skills", 3, "Combat tactique" },
                    { 2, "Développement", 3, "Ingénierie avancée" },
                    { 3, "Soft Skills", 2, "Super force" },
                    { 4, "Soft Skills", 3, "Discrétion" },
                    { 5, "Infrastructure", 2, "Hacking" }
                });

            migrationBuilder.InsertData(
                table: "Missions",
                columns: new[] { "Id", "Budget", "ClientId", "ConsultantId", "Description", "EndDate", "StartDate", "Title" },
                values: new object[,]
                {
                    { 1, 1000000m, 1, 1, "Protection de Gotham contre la pègre locale.", new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Défense de Gotham" },
                    { 2, 20000000m, 2, 2, "Déploiement de drones de défense autonomes.", new DateTime(2029, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iron Legion" },
                    { 3, 5000000m, 3, 3, "Mission diplomatique et maintien de la paix.", new DateTime(2030, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Paix mondiale" },
                    { 4, 8000000m, 3, 4, "Protection de Metropolis contre les attaques extraterrestres.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sauvetage métropolitain" },
                    { 5, 250000m, 4, 5, "Analyse et surveillance cybercriminelle.", new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Projet Arachné" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Missions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Missions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Missions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Missions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Missions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Consultants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Consultants",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Consultants",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Consultants",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Consultants",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
