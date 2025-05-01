using Data.Contexts;
using Data.Entities;
using Data.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace Data.Repositories;


public interface IProjectRepository : IBaseRepository<ProjectEntity, Project>
{
    Task<RepositoryResult<IEnumerable<Project>>> GetAllProjectsAsync(bool orderByDescending = false, Expression<Func<ProjectEntity, object>>? sortBy = null, Expression<Func<ProjectEntity, bool>>? where = null, params Expression<Func<ProjectEntity, object>>[] includes);

}
public class ProjectRepository(AppDbContext context) : BaseRepository<ProjectEntity, Project>(context), IProjectRepository
{

    public virtual async Task<RepositoryResult<IEnumerable<Project>>> GetAllProjectsAsync(bool orderByDescending = false, Expression<Func<ProjectEntity, object>>? sortBy = null, Expression<Func<ProjectEntity, bool>>? where = null, params Expression<Func<ProjectEntity, object>>[] includes)
    {
        IQueryable<ProjectEntity> query = _table;
        if (where != null)
        {
            query = query.Where(where);
        }
        if (includes != null && includes.Length != 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        if (sortBy != null)
        {
            query = orderByDescending ? query.OrderByDescending(sortBy) : query.OrderBy(sortBy);
        }

        var entities = await query.ToListAsync();
        var projects = new List<Project>();
        foreach (var entity in entities)
        {
            projects.Add(new Project
            {
                ProjectName = entity.ProjectName,
                Description = entity.Description,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Budget = entity.Budget,
                Image = entity.Image,
                Client = entity.Client != null ? new Client
                {
                    Id = entity.Client.Id,
                    ClientName = entity.Client.ClientName
                } : null!,
                Status = entity.Status != null ? new Status
                {
                    Id = entity.Status.Id,
                    StatusName = entity.Status.StatusName
                } : null!
            });
        }
        
        return new RepositoryResult<IEnumerable<Project>> { Succeeded = true, StatusCode = 200, Result = projects };
    }
}
