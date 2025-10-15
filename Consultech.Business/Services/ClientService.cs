using Consultech.Business.Abstractions;
using Consultech.Business.DTOs;
using Consultech.Business.Extensions;
using Consultech.DAL;
using Consultech.DAL.Entities;
using Consultech.DAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Consultech.Business.Services;

/// <summary>
/// Provides business logic for managing clients.
/// </summary>
internal sealed class ClientService(ConsultechDbContext dbContext) : IClientService
{
    /// <summary>
    /// Retrieves all clients with their associated missions.
    /// </summary>
    public async Task<List<ClientDto>> GetAll()
    {
        return await dbContext.Clients.AsNoTracking()
            .Include(c => c.Missions)
            .ThenInclude(m => m.Consultant)
            .Select(c => c.ToDto(true))
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a client by its unique identifier.
    /// </summary>
    public async Task<ClientDto?> GetById(int id)
    {
        var client = await dbContext.Clients.AsNoTracking()
            .Include(c => c.Missions)
            .ThenInclude(m => m.Consultant)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return client?.ToDto(true);
    }

    /// <summary>
    /// Creates a new client if it does not already exist.
    /// </summary>
    public async Task<int> Create(ClientDto client)
    {
        if (await dbContext.Clients.AnyAsync(c => c.Email == client.Email))
            throw new Exception("Un client avec cet e-mail existe déjà.");

        var clientToCreate = new Client
        {
            CompanyName = client.CompanyName,
            Email = client.Email,
            Address = client.Address,
            ActivitySector = client.ActivitySector,
        };
        
        dbContext.Clients.Add(clientToCreate);
        await dbContext.SaveChangesAsync();
        
        return clientToCreate.Id;
    }

    /// <summary>
    /// Updates an existing client with new information.
    /// </summary>
    public async Task<int> Update(ClientDto client)
    {
        var foundClient = await dbContext.Clients
            .Include(c => c.Missions)
            .FirstOrDefaultAsync(c => c.Id == client.Id);
    
        if (foundClient == null)
            throw new Exception("Aucun client trouvé.");

        foundClient.CompanyName = client.CompanyName;
        foundClient.Email = client.Email;
        foundClient.Address = client.Address;
        foundClient.ActivitySector = client.ActivitySector;

        var result = await dbContext.SaveChangesAsync();
        return result > 0 ? foundClient.Id : -1;
    }

    /// <summary>
    /// Deletes a client by its unique identifier.
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        var foundClient = await dbContext.Clients.FindAsync(id);
        if (foundClient == null)
            throw new KeyNotFoundException("Aucun client trouvé.");
        
        dbContext.Clients.Remove(foundClient);
        var operation = await dbContext.SaveChangesAsync();
        
        return operation > 0;
    }
    
    /// <summary>
    /// Retrieves all missions associated with a given client.
    /// </summary>
    public async Task<List<MissionDto>> GetMissionsByClient(int clientId)
    {
        return await dbContext.Missions.AsNoTracking()
            .Include(m => m.Consultant)
            .Include(m => m.Client)
            .Where(m => m.Client.Id == clientId)
            .Select(m => m.ToDto(true))
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves clients who currently have at least one active mission.
    /// </summary>
    public async Task<List<ClientDto>> GetActiveClients()
    {
        var today = DateTime.Today;

        return await dbContext.Clients.AsNoTracking()
            .Include(c => c.Missions)
            .ThenInclude(m => m.Consultant)
            .Where(c => c.Missions.Any(m => m.StartDate <= today && m.EndDate >= today))
            .Select(c => c.ToDto(true))
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves clients who have no current or recent missions.
    /// </summary>
    public async Task<List<ClientDto>> GetInactiveClients()
    {
        var today = DateTime.Today;

        return await dbContext.Clients.AsNoTracking()
            .Include(c => c.Missions)
            .Where(c => !c.Missions.Any(m => m.EndDate >= today.AddMonths(-3))) // inactif depuis 3 mois
            .Select(c => c.ToDto(true))
            .ToListAsync();
    }

    /// <summary>
    /// Searches clients by partial company name (case-insensitive).
    /// </summary>
    public async Task<List<ClientDto>> SearchByCompanyName(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new();

        query = query.Trim().ToLower();

        return await dbContext.Clients.AsNoTracking()
            .Include(c => c.Missions)
            .Where(c => EF.Functions.Like(c.CompanyName.ToLower(), $"%{query}%"))
            .Select(c => c.ToDto(true))
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves clients belonging to a specific activity sector.
    /// </summary>
    public async Task<List<ClientDto>> GetByActivitySector(ActivitySector sector)
    {
        return await dbContext.Clients.AsNoTracking()
            .Include(c => c.Missions)
            .ThenInclude(m => m.Consultant)
            .Where(c => c.ActivitySector == sector)
            .Select(c => c.ToDto(true))
            .ToListAsync();
    }
}
