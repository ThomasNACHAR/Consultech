using Consultech.Business.Abstractions;
using Consultech.Business.DTOs;
using Consultech.Business.Extensions;
using Consultech.DAL;
using Consultech.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Consultech.Business.Services;

internal sealed class SkillService(ConsultechDbContext dbContext) : ISkillService
{
    public async Task<List<SkillDto>> GetAll()
    {
        return await dbContext.Skills.Include(s => s.Consultants)
            .Select(s => s.ToDto())
            .ToListAsync();
    }

    public async Task<SkillDto?> GetById(int id)
    {
        var skill = await dbContext.Skills.Include(s => s.Consultants)
            .FirstOrDefaultAsync(s => s.Id == id);
        
        return skill?.ToDto();
    }

    public async Task<int> Create(SkillDto skill)
    {
        if (dbContext.Skills.Any(s => s.Title == skill.Title))
            throw new Exception("La compétence existe déjà.");

        var skillToCreate = new Skill()
        {
            Title = skill.Title,
            Category = skill.Category,
            Level = skill.Level,
        };
        
        dbContext.Skills.Add(skillToCreate);
        
        await dbContext.SaveChangesAsync();
        
        return skillToCreate.Id;
    }

    public async Task<int> Update(SkillDto skill)
    {
        var foundSkill = dbContext.Skills.Include(s => s.Consultants)
            .FirstOrDefault(s => s.Id == skill.Id);
        
        if (foundSkill == null)
            throw new Exception("Aucune compétence trouvée.");
        
        foundSkill.Title = skill.Title;
        foundSkill.Category = skill.Category;
        foundSkill.Level = skill.Level;
        
        dbContext.Skills.Update(foundSkill);
        var operation = await dbContext.SaveChangesAsync();
        return operation > 0 ? foundSkill.Id : -1;
    }

    public async Task<bool> Delete(int id)
    {
        var foundSkill = await dbContext.Skills.FindAsync(id);
        if (foundSkill == null)
            throw new Exception("Aucune compétence trouvée.");
        
        dbContext.Skills.Remove(foundSkill);
        var operation = await dbContext.SaveChangesAsync();
        
        return operation > 0;
    }
}