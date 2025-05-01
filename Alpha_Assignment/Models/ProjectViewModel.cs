using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Models;

public class ProjectViewModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Image { get; set; } = null!;
    public string ProjectName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IEnumerable<SelectListItem> Clients { get; set; } = [];
    public string ClientName { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? Budget { get; set; }
    public int StatusId { get; set; }
}
