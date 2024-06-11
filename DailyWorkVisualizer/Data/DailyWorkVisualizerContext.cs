using DailyWorkVisualizer.Models;
using Microsoft.EntityFrameworkCore;
namespace DailyWorkVisualizer.Data;

public class DailyWorkVisualizerContext: DbContext{
    public DbSet<Commit> Commits {get; set;}
    public DbSet<Day> Days {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data source=DailyWorkVisualizer.db");
    }
}
