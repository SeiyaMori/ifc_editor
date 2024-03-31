using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace IFC_Editor.Models;

public class Element
{
    public string ElementId { get; set; }
    public string Name { get; set; }
    public string ElementType { get; set; }
    public bool Exclude { get; set; }
}
