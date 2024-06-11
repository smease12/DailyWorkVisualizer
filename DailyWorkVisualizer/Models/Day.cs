namespace DailyWorkVisualizer.Models;

public class Day
{
    public int Id {get; set;}
    public DateTime Date {get; set;}
    public string Color {get; set;}

    public List<Commit> Commits {get; set;}
}
