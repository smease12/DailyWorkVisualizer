using DailyWorkVisualizer.Data;
using DailyWorkVisualizer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

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
        .Include(c => c.Commits).OrderBy(d => d.Date).ToList();
        mondays =  _dailyWorkVisualizerContext.Days.Where(d => d.DayOftheWeek == "Monday")
        .Include(c => c.Commits).OrderBy(d => d.Date).ToList();
        tuesdays = _dailyWorkVisualizerContext.Days.Where(d => d.DayOftheWeek == "Tuesday")
        .Include(c => c.Commits).OrderBy(d => d.Date).ToList();
        wednesdays = _dailyWorkVisualizerContext.Days.Where(d => d.DayOftheWeek == "Wednesday")
        .Include(c => c.Commits).OrderBy(d => d.Date).ToList();
        thursdays = _dailyWorkVisualizerContext.Days.Where(d => d.DayOftheWeek == "Thursday")
        .Include(c => c.Commits).OrderBy(d => d.Date).ToList();
        fridays = _dailyWorkVisualizerContext.Days.Where(d => d.DayOftheWeek == "Friday")
        .Include(c => c.Commits).OrderBy(d => d.Date).ToList();
        saturdays = _dailyWorkVisualizerContext.Days.Where(d => d.DayOftheWeek == "Saturday")
        .Include(c => c.Commits).OrderBy(d => d.Date).ToList();
    }

    public async Task<IActionResult> OnPostCommitAsync()
    {
        var emptyCommit = new Commit();
        emptyCommit.CommitDate = DateTime.Now;
        emptyCommit.Description = this.description;

        string today = DateTime.Now.ToString("yyyy-MM-dd");
        Day commitDay = _dailyWorkVisualizerContext.Days.Where(d => d.Date.ToString() == today)
        .FirstOrDefault();
        
        emptyCommit.DayId = commitDay.Id;
        if(commitDay.Commits == null)
            commitDay.Color = getDayColor(1);
        else
            commitDay.Color = getDayColor(commitDay.Commits.Count + 1);

        await _dailyWorkVisualizerContext.Commits.AddAsync(emptyCommit);
        await _dailyWorkVisualizerContext.SaveChangesAsync();

        loadDaysOftheWeek();

        return Page();
    }

    public string getDayColor(int commitCount)
    {
        string toReturn = "";

        if(commitCount < 1)
            toReturn = "grey";
        else if(commitCount < 3) 
            toReturn = "underThreeCommits";
        else if(commitCount < 5)
            toReturn = "underFiveCommits";
        else if(commitCount < 7)
            toReturn = "underSevenCommits";
        else if(commitCount > 7)
            toReturn = "SevenPlusCommits";

        return toReturn;
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
