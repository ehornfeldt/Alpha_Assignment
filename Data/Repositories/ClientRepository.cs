using Data.Contexts;
using Data.Entities;
using Domain.Models;

namespace Data.Repositories;

public interface IClientRepository : IBaseRepository<ClientEntity>
{
}

public class ClientRepository(AppDbContext context) : BaseRepository<ClientEntity, Client>(context), IClientRepository
{
}
