using Business.Models;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;

namespace Business.Services;

public interface IClientService
{
    Task<ClientResult> GetClientsAsync();
    Task<ClientResult<Client>> GetClientByIdAsync(string id);
}

public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<ClientResult> GetClientsAsync()
    {
        var result = await _clientRepository.GetAllAsync();
        return result.MapTo<ClientResult>();
    }

    public async Task<ClientResult<Client>> GetClientByIdAsync(string id)
    {
        var result = await _clientRepository.GetAsync(x => x.Id == id);
        return result.Succeeded
            ? new ClientResult<Client> { Succeeded = true, StatusCode = 200, Result = result.Result }
            : new ClientResult<Client> { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
    }
}
