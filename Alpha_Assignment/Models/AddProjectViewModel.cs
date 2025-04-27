using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Models;

public class AddProjectViewModel
{
    public IEnumerable<SelectListItem> Clients { get; set; } = [];
}
