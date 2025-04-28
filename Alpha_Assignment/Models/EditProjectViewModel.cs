using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Models;

public class EditProjectViewModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public IEnumerable<SelectListItem> Clients { get; set; } = [];
    public IEnumerable<SelectListItem> Statuses { get; set; } = [];

    public string ProjectName { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? Budget { get; set; }
}
