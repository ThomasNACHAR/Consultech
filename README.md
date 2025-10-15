# 🏛️ Consultech — API de gestion des missions et des consultants

## 📖 Présentation

**Consultech** est une application backend développée en **ASP.NET Core 9** permettant de gérer les **consultants**, **clients**, **missions** et **compétences** au sein d’une société de conseil.

Ce projet a été réalisé dans le cadre du **TP final de la formation Concepteur Développeur d’Applications (AJC Formation)**.  
Il illustre la conception d’une API REST complète avec gestion de base de données, relations entités et documentation Swagger.

---

## ⚙️ Stack technique

| Technologie | Version / Description |
|--------------|------------------------|
| 🧩 Framework | .NET 9 |
| 🗄️ ORM | Entity Framework Core 9 |
| 💽 Base de données | Microsoft SQL Server |
| 🧰 IDE | Visual Studio 2022 |
| 📜 Documentation API | Swagger / OpenAPI |
| 💻 Langage | C# 12 |

---

## 🧩 Architecture du projet

tpfinalConsultech/
│
├── Consultech.API/ # Couche API (contrôleurs, configuration)
│ ├── Controllers/
│ │ ├── ClientsController.cs
│ │ ├── ConsultantsController.cs
│ │ ├── MissionsController.cs
│ │ └── SkillsController.cs
│ ├── Program.cs # Configuration de l’application
│ ├── appsettings.json # Connexion à la base ConsultTechDB
│
├── Consultech.DAL/ # Couche d’accès aux données (Entities + DbContext)
│ ├── Entities/
│ │ ├── Client.cs
│ │ ├── Consultant.cs
│ │ ├── Mission.cs
│ │ ├── Skill.cs
│ │ └── Enums/
│ │ ├── ActivitySector.cs
│ │ └── SkillLevel.cs
│ ├── ConsultTechDbContext.cs # Contexte EF Core
│
└── README.md # Documentation du projet

## 👥 Auteurs

👨‍💻 Pierre DELAROCQUE
👨‍💻 Thomas NACHAR
👨‍💻 Kevin GUILLOT
