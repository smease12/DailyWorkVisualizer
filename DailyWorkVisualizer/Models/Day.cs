namespace DailyWorkVisualizer.Models;

public class Day
{
    public int Id {get; set;}
    public DateTime Date {get; set;}
    public string Color {get; set;}
    public string DayOftheWeek {get; set;}

    public List<Commit> Commits {get; set;}

    public int numCommits()
    {
        if(Commits == null)
            return 0;
        else
            return this.Commits.Count;
    }
}
