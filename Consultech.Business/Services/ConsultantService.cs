using Consultech.Business.Abstractions;
using Consultech.Business.DTOs;
using Consultech.Business.Extensions;
using Consultech.DAL;
using Consultech.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Consultech.Business.Services;

/// <summary>
/// Provides business logic for managing consultants.
/// </summary>
internal sealed class ConsultantService(ConsultechDbContext dbContext) : IConsultantService
{
    /// <summary>
    /// Retrieves all consultants with their associated skills.
    /// </summary>
    public async Task<List<ConsultantDto>> GetAll()
    {
        return await dbContext.Consultants.AsNoTracking()
            .Include(c => c.Skills)
            .Select(c => c.ToDto(true))
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a consultant by their unique identifier.
    /// </summary>
    public async Task<ConsultantDto?> GetById(int id)
    {
        var consultant = await dbContext.Consultants.AsNoTracking()
            .Include(c => c.Skills)
            .FirstOrDefaultAsync(c => c.Id == id);

        return consultant?.ToDto();
    }

    /// <summary>
    /// Creates a new consultant record.
    /// </summary>
    public async Task<int> Create(ConsultantDto consultant)
    {
        if (await dbContext.Consultants.AnyAsync(c => c.Email == consultant.Email))
            throw new Exception("Un consultant avec cet e-mail existe déjà.");

        var consultantToCreate = new Consultant
        {
            FirstName = consultant.FirstName,
            LastName = consultant.LastName,
            Email = consultant.Email,
            StartDate = consultant.StartDate,
            IsAvailable = consultant.IsAvailable,
        };

        dbContext.Consultants.Add(consultantToCreate);
        await dbContext.SaveChangesAsync();
        await this.AssignSkills(consultantToCreate.Id, consultant.Skills.Select(s => s.Id).ToList());
        return consultantToCreate.Id;
    }

    /// <summary>
    /// Updates an existing consultant’s information.
    /// </summary>
    public async Task<int> Update(ConsultantDto consultant)
    {
        var foundConsultant = await dbContext.Consultants
            .Include(c => c.Skills)
            .FirstOrDefaultAsync(c => c.Id == consultant.Id);

        if (foundConsultant == null)
            throw new Exception("Aucun consultant trouvé.");

        foundConsultant.FirstName = consultant.FirstName;
        foundConsultant.LastName = consultant.LastName;
        foundConsultant.Email = consultant.Email;
        foundConsultant.StartDate = consultant.StartDate;
        foundConsultant.IsAvailable = consultant.IsAvailable;

        var result = await dbContext.SaveChangesAsync();
        await this.AssignSkills(foundConsultant.Id, consultant.Skills.Select(s => s.Id).ToList());
        return result > 0 ? foundConsultant.Id : -1;
    }

    /// <summary>
    /// Deletes a consultant by their unique identifier.
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        var foundConsultant = await dbContext.Consultants.FindAsync(id);
        if (foundConsultant == null)
            throw new KeyNotFoundException("Aucun consultant trouvé.");

        dbContext.Consultants.Remove(foundConsultant);
        var operation = await dbContext.SaveChangesAsync();

        return operation > 0;
    }

    /// <summary>
    /// Assigns one or more skills to a consultant.
    /// </summary>
    private async Task<bool> AssignSkills(int consultantId, List<int> skillIds)
    {
        var consultant = await dbContext.Consultants
            .Include(c => c.Skills)
            .FirstOrDefaultAsync(c => c.Id == consultantId);

        if (consultant == null)
            throw new Exception("Aucun consultant trouvé.");

        var skills = await dbContext.Skills
            .Where(s => skillIds.Contains(s.Id))
            .ToListAsync();

        consultant.Skills = skills;
        var result = await dbContext.SaveChangesAsync();

        return result > 0;
    }

    /// <summary>
    /// Retrieves all consultants currently available for new missions.
    /// </summary>
    public async Task<List<ConsultantDto>> GetAvailableConsultants()
    {
        return await dbContext.Consultants.AsNoTracking()
            .Where(c => c.IsAvailable)
            .Include(c => c.Skills)
            .Select(c => c.ToDto(true))
            .ToListAsync();
    }
}
