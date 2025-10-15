using Consultech.DAL.Entities;
using Consultech.DAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Consultech.DAL;

public static class ConsultechDbContextSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // --- CLIENTS (ENTREPRISES) ---
        modelBuilder.Entity<Client>().HasData(
            new Client { Id = 1, CompanyName = "Wayne Enterprises", Email = "contact@wayneenterprises.com", Address = "1007 Mountain Drive, Gotham", ActivitySector = ActivitySector.Manufacturing },
            new Client { Id = 2, CompanyName = "Stark Industries", Email = "info@starkindustries.com", Address = "200 Park Avenue, New York", ActivitySector = ActivitySector.Energy },
            new Client { Id = 3, CompanyName = "Daily Planet", Email = "editor@dailyplanet.com", Address = "1 Planet Plaza, Metropolis", ActivitySector = ActivitySector.Telecommunications },
            new Client { Id = 4, CompanyName = "Oscorp Technologies", Email = "admin@oscorp.com", Address = "Norman Tower, New York", ActivitySector = ActivitySector.InformationTechnology },
            new Client { Id = 5, CompanyName = "Queen Consolidated", Email = "hq@queenconsolidated.com", Address = "Star City Central Ave", ActivitySector = ActivitySector.Energy }
        );

        // --- CONSULTANTS (HÉROS) ---
        modelBuilder.Entity<Consultant>().HasData(
            new Consultant { Id = 1, FirstName = "Bruce", LastName = "Wayne", Email = "bruce@wayneenterprises.com", StartDate = new DateTime(2010, 5, 1), IsAvailable = true },
            new Consultant { Id = 2, FirstName = "Tony", LastName = "Stark", Email = "tony@starkindustries.com", StartDate = new DateTime(2012, 3, 10), IsAvailable = false },
            new Consultant { Id = 3, FirstName = "Diana", LastName = "Prince", Email = "diana@themiscira.org", StartDate = new DateTime(2015, 6, 20), IsAvailable = true },
            new Consultant { Id = 4, FirstName = "Clark", LastName = "Kent", Email = "clark@dailyplanet.com", StartDate = new DateTime(2011, 9, 15), IsAvailable = false },
            new Consultant { Id = 5, FirstName = "Peter", LastName = "Parker", Email = "peter@dailybugle.com", StartDate = new DateTime(2018, 1, 1), IsAvailable = true }
        );

        // --- SKILLS (COMPÉTENCES) ---
        modelBuilder.Entity<Skill>().HasData(
            new Skill { Id = 1, Title = "Combat tactique", Category = "Soft Skills", Level = SkillLevel.Expert },
            new Skill { Id = 2, Title = "Ingénierie avancée", Category = "Développement", Level = SkillLevel.Expert },
            new Skill { Id = 3, Title = "Super force", Category = "Soft Skills", Level = SkillLevel.Advanced },
            new Skill { Id = 4, Title = "Discrétion", Category = "Soft Skills", Level = SkillLevel.Expert },
            new Skill { Id = 5, Title = "Hacking", Category = "Infrastructure", Level = SkillLevel.Advanced }
        );

        // --- MISSIONS (PROJETS) ---
        modelBuilder.Entity<Mission>().HasData(
            new Mission
            {
                Id = 1,
                Title = "Défense de Gotham",
                Description = "Protection de Gotham contre la pègre locale.",
                StartDate = new DateTime(2023, 1, 10),
                EndDate = new DateTime(2023, 6, 10),
                Budget = 1000000,
                ClientId = 1,
                ConsultantId = 1
            },
            new Mission
            {
                Id = 2,
                Title = "Iron Legion",
                Description = "Déploiement de drones de défense autonomes.",
                StartDate = new DateTime(2026, 4, 1),
                EndDate = new DateTime(2029, 4, 1),
                Budget = 20000000,
                ClientId = 2,
                ConsultantId = 2
            },
            new Mission
            {
                Id = 3,
                Title = "Paix mondiale",
                Description = "Mission diplomatique et maintien de la paix.",
                StartDate = new DateTime(2027, 1, 1),
                EndDate = new DateTime(2030, 12, 31),
                Budget = 5000000,
                ClientId = 3,
                ConsultantId = 3
            },
            new Mission
            {
                Id = 4,
                Title = "Sauvetage métropolitain",
                Description = "Protection de Metropolis contre les attaques extraterrestres.",
                StartDate = new DateTime(2025, 11, 1),
                EndDate = new DateTime(2026, 1, 1),
                Budget = 8000000,
                ClientId = 3,
                ConsultantId = 4
            },
            new Mission
            {
                Id = 5,
                Title = "Projet Arachné",
                Description = "Analyse et surveillance cybercriminelle.",
                StartDate = new DateTime(2024, 9, 1),
                EndDate = new DateTime(2025, 3, 1),
                Budget = 250000,
                ClientId = 4,
                ConsultantId = 5
            }
        );
        
        // --- CONSULTANT <-> SKILL (RELATION N-N) ---
        modelBuilder.Entity("ConsultantSkill").HasData(
            new { ConsultantsId = 1, SkillsId = 1 },
            new { ConsultantsId = 1, SkillsId = 4 },
            new { ConsultantsId = 2, SkillsId = 2 },
            new { ConsultantsId = 2, SkillsId = 5 },
            new { ConsultantsId = 3, SkillsId = 3 },
            new { ConsultantsId = 3, SkillsId = 1 },
            new { ConsultantsId = 4, SkillsId = 3 },
            new { ConsultantsId = 5, SkillsId = 5 },
            new { ConsultantsId = 5, SkillsId = 4 }
        );
    }
}
