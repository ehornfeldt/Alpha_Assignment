﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class AddProjectViewModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [Display(Name = "Project Name", Prompt = "Project name")]
    [DataType(DataType.Text)]
    public string ProjectName { get; set; } = null!;

    public IEnumerable<SelectListItem> Clients { get; set; } = [];

    [Required]
    [Display(Name = "Client")]
    public string ClientId { get; set; } = null!;

    [Required]
    [Display(Name = "Description", Prompt = "Enter description")]
    [DataType(DataType.MultilineText)]
    public string Description { get; set; } = null!;

    [Required]
    [Display(Name = "Start Date")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Display(Name = "End Date")]
    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

    [Display(Name = "Budget")]
    [DataType(DataType.Currency)]
    [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive number")]
    public decimal? Budget { get; set; }

}
