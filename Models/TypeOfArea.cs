using System;
using System.Collections.Generic;

namespace DotNetCoreSqlDb.Models;

public partial class TypeOfArea
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? Name { get; set; }

    public string? NameNl { get; set; }

    public string? NameFr { get; set; }

    public string? NameGe { get; set; }

    public string? UrlWikipedia { get; set; }

    public string? UrlWikipediaNl { get; set; }

    public string? UrlWikipediaFr { get; set; }

    public string? UrlWikipediaGe { get; set; }

    public virtual ICollection<Area> Areas { get; set; } = new List<Area>();
}
