using Consultech.Business.DTOs;
using Consultech.DAL.Entities.Enums;

namespace Consultech.Business.Abstractions;

public interface IClientService
{
    Task<List<ClientDto>> GetAll();
    
    Task<ClientDto?> GetById(int id);
    
    Task<int> Create(ClientDto client);
    
    Task<int> Update(ClientDto client);
    
    Task<bool> Delete(int id);
    
    Task<List<MissionDto>> GetMissionsByClient(int clientId);

    Task<List<ClientDto>> GetActiveClients();

    Task<List<ClientDto>> GetInactiveClients();

    Task<List<ClientDto>> SearchByCompanyName(string query);

    Task<List<ClientDto>> GetByActivitySector(ActivitySector sector);
}