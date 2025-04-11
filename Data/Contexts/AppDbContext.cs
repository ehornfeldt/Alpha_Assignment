using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<ProjectEntity> Projects { get; set; } = null!;
    public DbSet<ClientEntity> Clients { get; set; } = null!;
    public DbSet<StatusEntity> Statuses { get; set; } = null!;
}
