using DailyWorkVisualizer.Data;
using DailyWorkVisualizer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DailyWorkVisualizer.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private DailyWorkVisualizerContext _dailyWorkVisualizerContext;

    public IndexModel(ILogger<IndexModel> logger, DailyWorkVisualizerContext dailyWorkVisualizerContext)
    {
        _logger = logger;
        _dailyWorkVisualizerContext = dailyWorkVisualizerContext;
    }
    [BindProperty]
    public string description {get; set;}
    [BindProperty]
    public List<Day> sundays {get; set;}
    [BindProperty]
    public List<Day> mondays {get; set;}
    [BindProperty]
    public List<Day> tuesdays {get; set;}
    [BindProperty]
    public List<Day> wednesdays {get; set;}
    [BindProperty]
    public List<Day> thursdays {get; set;}
    [BindProperty]
    public List<Day> fridays {get; set;}
    [BindProperty]
    public List<Day> saturdays {get; set;}
    public void OnGet()
    {
        sundays = _dailyWorkVisualizerContext.Days.Where(d => d.DayOftheWeek == "Sunday")
        .OrderByDescending(d => d.Date).ToList();
        mondays = _dailyWorkVisualizerContext.Days.Where(d => d.DayOftheWeek == "Monday")
        .OrderByDescending(d => d.Date).ToList();
    }

    public void OnPostCommit()
    {
        var emptyCommit = new Commit();
        emptyCommit.CommitDate = DateTime.Now;
        emptyCommit.Description = this.description;

        _dailyWorkVisualizerContext.Commits.Add(emptyCommit);
        _dailyWorkVisualizerContext.SaveChanges();
    }

        public void OnPostToDo()
    {
        var emptyToDo = new ToDo();
        emptyToDo.ToDoDate = DateTime.Now;
        emptyToDo.Description = this.description;

        _dailyWorkVisualizerContext.ToDos.Add(emptyToDo);
        _dailyWorkVisualizerContext.SaveChanges();
    }
}
