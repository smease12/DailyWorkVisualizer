using DailyWorkVisualizer.Data;
using DailyWorkVisualizer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> OnGetAsync()
    {
        loadDaysOftheWeek();
        return Page();
    }

    public void loadDaysOftheWeek(){
        sundays =  _dailyWorkVisualizerContext.Days.Where(d => d.DayOftheWeek == "Sunday")
        .OrderByDescending(d => d.Date).ToList();
        mondays =  _dailyWorkVisualizerContext.Days.Where(d => d.DayOftheWeek == "Monday")
        .OrderByDescending(d => d.Date).ToList();
        tuesdays = _dailyWorkVisualizerContext.Days.Where(d => d.DayOftheWeek == "Tuesday")
        .OrderByDescending(d => d.Date).ToList();
        wednesdays = _dailyWorkVisualizerContext.Days.Where(d => d.DayOftheWeek == "Wednesday")
        .OrderByDescending(d => d.Date).ToList();
        thursdays = _dailyWorkVisualizerContext.Days.Where(d => d.DayOftheWeek == "Thursday")
        .OrderByDescending(d => d.Date).ToList();
        fridays = _dailyWorkVisualizerContext.Days.Where(d => d.DayOftheWeek == "Friday")
        .OrderByDescending(d => d.Date).ToList();
        saturdays = _dailyWorkVisualizerContext.Days.Where(d => d.DayOftheWeek == "Saturday")
        .OrderByDescending(d => d.Date).ToList();
    }

    public async Task<IActionResult> OnPostCommitAsync()
    {
        var emptyCommit = new Commit();
        emptyCommit.CommitDate = DateTime.Now;
        emptyCommit.Description = this.description;

        await _dailyWorkVisualizerContext.Commits.AddAsync(emptyCommit);
        await _dailyWorkVisualizerContext.SaveChangesAsync();

        loadDaysOftheWeek();

        return Page();
    }

        public async Task<IActionResult> OnPostToDoAsync()
    {
        var emptyToDo = new ToDo();
        emptyToDo.ToDoDate = DateTime.Now;
        emptyToDo.Description = this.description;

        await _dailyWorkVisualizerContext.ToDos.AddAsync(emptyToDo);
        await _dailyWorkVisualizerContext.SaveChangesAsync();

        loadDaysOftheWeek();
        
        return Page();
    }
}
