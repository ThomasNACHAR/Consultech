using Consultech.Business.DTOs;
using Consultech.DAL.Entities;

namespace Consultech.Business.Extensions;

public static class MappingExtensions
{
    // ===================== Skill =====================
    public static SkillDto ToDto(this Skill skill)
    {
        return new SkillDto
        {
            Id = skill.Id,
            Title = skill.Title,
            Category = skill.Category,
            Level = skill.Level,
            Consultants = skill.Consultants?.Select(c => c.ToDto(false)).ToList() ?? new List<ConsultantDto>()
        };
    }

    // ===================== Consultant =====================
    public static ConsultantDto ToDto(this Consultant consultant, bool mapSkills = true)
    {
        return new ConsultantDto
        {
            Id = consultant.Id,
            FirstName = consultant.FirstName,
            LastName = consultant.LastName,
            Email = consultant.Email,
            StartDate = consultant.StartDate,
            IsAvailable = consultant.IsAvailable,
            Skills = mapSkills
                ? consultant.Skills?.Select(s => s.ToDto()).ToList() ?? new List<SkillDto>()
                : new List<SkillDto>()
        };
    }

    // ===================== Client =====================
    public static ClientDto ToDto(this Client client, bool mapMissions = true)
    {
        return new ClientDto
        {
            Id = client.Id,
            CompanyName = client.CompanyName,
            Email = client.Email,
            Address = client.Address,
            ActivitySector = client.ActivitySector,
            Missions = mapMissions
                ? client.Missions?.Select(m => m.ToDto(false)).ToList() ?? new List<MissionDto>()
                : new List<MissionDto>()
        };
    }

    // ===================== Mission =====================
    public static MissionDto ToDto(this Mission mission, bool mapNested = true)
    {
        return new MissionDto
        {
            Id = mission.Id,
            Title = mission.Title,
            Description = mission.Description,
            StartDate = mission.StartDate,
            EndDate = mission.EndDate,
            Budget = mission.Budget,
            Client = mapNested ? mission.Client.ToDto(false) : new ClientDto(),
            Consultant = mapNested ? mission.Consultant.ToDto(false) : new ConsultantDto()
        };
    }
}
