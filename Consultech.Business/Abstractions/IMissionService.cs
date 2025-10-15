using Consultech.Business.DTOs;

namespace Consultech.Business.Abstractions;

public interface IMissionService
{
    Task<List<MissionDto>> GetAll();
    
    Task<MissionDto?> GetById(int id);
    
    Task<int> Create(MissionDto mission);
    
    Task<int> Update(MissionDto mission);
    
    Task<bool> Delete(int id);

    Task<List<MissionDto>> GetByClientId(int clientId);

    Task<List<MissionDto>> GetByConsultantId(int consultantId);

    Task<bool> AssignConsultant(int missionId, int consultantId);

    Task<bool> UnassignConsultant(int missionId);

    Task<List<MissionDto>> GetActiveMissions();

    Task<List<MissionDto>> GetCompletedMissions();

    Task<List<MissionDto>> GetUpcomingMissions();
}