namespace DailyWorkVisualizer.Models;

public class Commit
{
    public int Id {get; set;}
    public DateTime CommitDate {get; set;}
    public string Description {get; set;}

    public int DayId{get; set;}
}
