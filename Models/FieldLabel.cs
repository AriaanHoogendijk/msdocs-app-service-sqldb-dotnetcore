using System;
using System.Collections.Generic;

namespace DotNetCoreSqlDb.Models;

public partial class FieldLabel
{
    public int Id { get; set; }

    public string? FieldName { get; set; }

    public string? LabelOfField { get; set; }
}
