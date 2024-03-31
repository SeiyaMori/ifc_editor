using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


public class ElementContext : DbContext
{
    public DbSet<Element> Elements { get; set; }

    public string DbPath { get; }

    public ElementContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "element.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}


public class Element
{
    public string ElementId { get; set; }
    public string Name { get; set; }
    public string ElementType { get; set; }
    public bool Exclude { get; set; }
}
