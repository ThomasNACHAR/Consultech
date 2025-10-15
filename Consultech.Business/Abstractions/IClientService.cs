using Consultech.Business.DTOs;

namespace Consultech.Business.Abstractions;

public interface IClientService
{
    Task<List<ClientDto>> GetAll();
    
    Task<ClientDto?> GetById(int id);
    
    Task<int> Create(ClientDto client);
    
    Task<int> Update(ClientDto client);
    
    Task<bool> Delete(int id);
}