using Consultech.Business.DTOs;

namespace Consultech.Business.Abstractions;

public interface ISkillService
{
    Task<List<SkillDto>> GetAll();
    
    Task<SkillDto?> GetById(int id);
    
    Task<int> Create(SkillDto skill);
    
    Task<int> Update(SkillDto skill);
    
    Task<bool> Delete(int id);
}