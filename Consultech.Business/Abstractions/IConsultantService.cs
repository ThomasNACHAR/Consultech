using Consultech.Business.DTOs;

namespace Consultech.Business.Abstractions;

public interface IConsultantService
{
    Task<List<ConsultantDto>> GetAll();
    
    Task<ConsultantDto?> GetById(int id);

    Task<int> Create(ConsultantDto consultant);

    Task<int> Update(ConsultantDto consultant);

    Task<bool> Delete(int id);

    Task<bool> AssignSkills(int consultantId, List<int> skillIds);

    Task<List<ConsultantDto>> GetAvailableConsultants();
}