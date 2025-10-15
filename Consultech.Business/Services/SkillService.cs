using Consultech.Business.Abstractions;
using Consultech.Business.DTOs;
using Consultech.Business.Extensions;
using Consultech.DAL;
using Consultech.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Consultech.Business.Services;

/// <summary>
/// Provides business logic for managing skills.
/// </summary>
internal sealed class SkillService(ConsultechDbContext dbContext) : ISkillService
{
    /// <summary>
    /// Retrieves all skills with their associated consultants.
    /// </summary>
    public async Task<List<SkillDto>> GetAll()
    {
        return await dbContext.Skills.AsNoTracking()
            .Include(s => s.Consultants)
            .Select(s => s.ToDto())
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a skill by its unique identifier.
    /// </summary>
    public async Task<SkillDto?> GetById(int id)
    {
        var skill = await dbContext.Skills.AsNoTracking()
            .Include(s => s.Consultants)
            .FirstOrDefaultAsync(s => s.Id == id);
        
        return skill?.ToDto();
    }

    /// <summary>
    /// Creates a new skill if it does not already exist.
    /// </summary>
    public async Task<int> Create(SkillDto skill)
    {
        if (await dbContext.Skills.AnyAsync(s => s.Title == skill.Title))
            throw new Exception("Skill already exists.");

        var skillToCreate = new Skill
        {
            Title = skill.Title,
            Category = skill.Category,
            Level = skill.Level,
        };
        
        dbContext.Skills.Add(skillToCreate);
        await dbContext.SaveChangesAsync();
        
        return skillToCreate.Id;
    }

    /// <summary>
    /// Updates an existing skill with new information.
    /// </summary>
    public async Task<int> Update(SkillDto skill)
    {
        var foundSkill = await dbContext.Skills
            .Include(s => s.Consultants)
            .FirstOrDefaultAsync(s => s.Id == skill.Id);
    
        if (foundSkill == null)
            throw new Exception("Skill not found.");

        foundSkill.Title = skill.Title;
        foundSkill.Category = skill.Category;
        foundSkill.Level = skill.Level;

        var result = await dbContext.SaveChangesAsync();
        return result > 0 ? foundSkill.Id : -1;
    }

    /// <summary>
    /// Deletes a skill by its unique identifier.
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        var foundSkill = await dbContext.Skills.FindAsync(id);
        if (foundSkill == null)
            throw new KeyNotFoundException("Skill not found.");
        
        dbContext.Skills.Remove(foundSkill);
        var operation = await dbContext.SaveChangesAsync();
        
        return operation > 0;
    }
}
