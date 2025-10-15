using Consultech.Business.Abstractions;
using Consultech.Business.DTOs;
using Consultech.Business.Extensions;
using Consultech.DAL;
using Consultech.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Consultech.Business.Services;

/// <summary>
/// Provides business logic for managing missions.
/// </summary>
internal sealed class MissionService(ConsultechDbContext dbContext) : IMissionService
{
    /// <summary>
    /// Retrieves all missions with their related client and consultant.
    /// </summary>
    public async Task<List<MissionDto>> GetAll()
    {
        return await dbContext.Missions.AsNoTracking()
            .Include(m => m.Client)
            .Include(m => m.Consultant)
            .Select(m => m.ToDto(true))
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a mission by its unique identifier.
    /// </summary>
    public async Task<MissionDto?> GetById(int id)
    {
        var mission = await dbContext.Missions.AsNoTracking()
            .Include(m => m.Client)
            .Include(m => m.Consultant)
            .FirstOrDefaultAsync(m => m.Id == id);

        return mission?.ToDto(true);
    }

    /// <summary>
    /// Creates a new mission.
    /// </summary>
    public async Task<int> Create(MissionDto mission)
    {
        if (mission.EndDate < mission.StartDate)
            throw new Exception("End date cannot be earlier than start date.");

        // Récupère les entités associées existantes
        var client = await dbContext.Clients.FindAsync(mission.Client.Id)
                     ?? throw new Exception("Client not found.");

        var missionToCreate = new Mission
        {
            Title = mission.Title,
            Description = mission.Description,
            StartDate = mission.StartDate,
            EndDate = mission.EndDate,
            Budget = mission.Budget,
            Client = client
        };

        dbContext.Missions.Add(missionToCreate);
        await dbContext.SaveChangesAsync();

        return missionToCreate.Id;
    }

    /// <summary>
    /// Updates an existing mission.
    /// </summary>
    public async Task<int> Update(MissionDto mission)
    {
        var foundMission = await dbContext.Missions
            .Include(m => m.Client)
            .Include(m => m.Consultant)
            .FirstOrDefaultAsync(m => m.Id == mission.Id);

        if (foundMission == null)
            throw new Exception("Mission not found.");

        foundMission.Title = mission.Title;
        foundMission.Description = mission.Description;
        foundMission.StartDate = mission.StartDate;
        foundMission.EndDate = mission.EndDate;
        foundMission.Budget = mission.Budget;

        var client = await dbContext.Clients.FindAsync(mission.Client.Id)
                     ?? throw new Exception("Client not found.");

        foundMission.Client = client;

        var result = await dbContext.SaveChangesAsync();
        return result > 0 ? foundMission.Id : -1;
    }

    /// <summary>
    /// Deletes a mission by its unique identifier.
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        var foundMission = await dbContext.Missions.FindAsync(id);
        if (foundMission == null)
            throw new KeyNotFoundException("Mission not found.");

        dbContext.Missions.Remove(foundMission);
        var result = await dbContext.SaveChangesAsync();

        return result > 0;
    }

    /// <summary>
    /// Retrieves all missions for a specific client.
    /// </summary>
    public async Task<List<MissionDto>> GetByClientId(int clientId)
    {
        return await dbContext.Missions.AsNoTracking()
            .Include(m => m.Client)
            .Include(m => m.Consultant)
            .Where(m => m.Client.Id == clientId)
            .Select(m => m.ToDto(true))
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves all missions for a specific consultant.
    /// </summary>
    public async Task<List<MissionDto>> GetByConsultantId(int consultantId)
    {
        return await dbContext.Missions.AsNoTracking()
            .Include(m => m.Client)
            .Include(m => m.Consultant)
            .Where(m => m.Consultant.Id == consultantId)
            .Select(m => m.ToDto(true))
            .ToListAsync();
    }

    /// <summary>
    /// Assigns a consultant to a mission.
    /// </summary>
    public async Task<bool> AssignConsultant(int missionId, int consultantId)
    {
        var mission = await dbContext.Missions
            .Include(m => m.Consultant)
            .FirstOrDefaultAsync(m => m.Id == missionId);

        if (mission == null)
            throw new Exception("Mission not found.");

        var consultant = await dbContext.Consultants.FindAsync(consultantId)
                          ?? throw new Exception("Consultant not found.");

        mission.Consultant = consultant;
        var result = await dbContext.SaveChangesAsync();

        return result > 0;
    }

    /// <summary>
    /// Removes the consultant from a mission.
    /// </summary>
    public async Task<bool> UnassignConsultant(int missionId)
    {
        var mission = await dbContext.Missions
            .Include(m => m.Consultant)
            .FirstOrDefaultAsync(m => m.Id == missionId);

        if (mission == null)
            throw new Exception("Mission not found.");

        mission.Consultant = null;
        var result = await dbContext.SaveChangesAsync();

        return result > 0;
    }

    /// <summary>
    /// Retrieves active missions (current date between start and end).
    /// </summary>
    public async Task<List<MissionDto>> GetActiveMissions()
    {
        var now = DateTime.Today;
        return await dbContext.Missions.AsNoTracking()
            .Include(m => m.Client)
            .Include(m => m.Consultant)
            .Where(m => m.StartDate <= now && m.EndDate >= now)
            .Select(m => m.ToDto(true))
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves completed missions (end date passed).
    /// </summary>
    public async Task<List<MissionDto>> GetCompletedMissions()
    {
        var now = DateTime.Today;
        return await dbContext.Missions.AsNoTracking()
            .Include(m => m.Client)
            .Include(m => m.Consultant)
            .Where(m => m.EndDate < now)
            .Select(m => m.ToDto(true))
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves upcoming missions (start date in the future).
    /// </summary>
    public async Task<List<MissionDto>> GetUpcomingMissions()
    {
        var now = DateTime.Today;
        return await dbContext.Missions.AsNoTracking()
            .Include(m => m.Client)
            .Include(m => m.Consultant)
            .Where(m => m.StartDate > now)
            .Select(m => m.ToDto(true))
            .ToListAsync();
    }
}
