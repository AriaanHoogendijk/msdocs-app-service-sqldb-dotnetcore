using System;
using System.Collections.Generic;

namespace DotNetCoreSqlDb.Models;

public partial class Area
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string NameNl { get; set; } = null!;

    public string NameFr { get; set; } = null!;

    public string NameGe { get; set; } = null!;

    public string? UrlWikipedia { get; set; }

    public string? UrlWikipediaNl { get; set; }

    public string? UrlWikipediaFr { get; set; }

    public string? UrlWikipediaGe { get; set; }

    public string Description { get; set; } = null!;

    public int TypeOfAreaId { get; set; }

    public string? DateFrom { get; set; }

    public string? DateTo { get; set; }
    public virtual TypeOfArea TypeOfArea { get; set; } = null!;
}