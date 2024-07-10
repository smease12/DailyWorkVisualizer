using DailyWorkVisualizer.Data;
using DailyWorkVisualizer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DailyWorkVisualizer.Pages;

public class ToDoModel : PageModel
{
    private readonly ILogger<ToDoModel> _logger;
    private readonly DailyWorkVisualizerContext _dailyWorkVisualizerContext;

    [BindProperty]
    public List<ToDo> toDos {get; set;}
    [BindProperty]
    public string description {get; set;}

    public ToDoModel(ILogger<ToDoModel> logger, DailyWorkVisualizerContext dailyWorkVisualizerContext)
    {
        _logger = logger;
        _dailyWorkVisualizerContext = dailyWorkVisualizerContext;
    }

    public void OnGet()
    {
        toDos = _dailyWorkVisualizerContext.ToDos.OrderByDescending(c => c.ToDoDate).ToList();
    }

    public async Task<IActionResult> OnPostMarkAsDone(int id)
    {
        var task = _dailyWorkVisualizerContext.ToDos.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            task.isDone = true;
        }

        createCommit(task);

        _dailyWorkVisualizerContext.Update(task);
        _dailyWorkVisualizerContext.SaveChanges();
        toDos = _dailyWorkVisualizerContext.ToDos.OrderByDescending(c => c.ToDoDate).ToList();

        return RedirectToPage();
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

    public void createCommit(ToDo task){
        var emptyCommit = new Commit();
        emptyCommit.CommitDate = DateTime.Now;
        emptyCommit.Description = task.Description;

        string today = DateTime.Now.ToString("yyyy-MM-dd");
        Day commitDay = _dailyWorkVisualizerContext.Days.Where(d => d.Date.ToString() == today)
        .FirstOrDefault();
        emptyCommit.DayId = commitDay.Id;

        if(commitDay.Commits == null)
            commitDay.Color = getDayColor(1);
        else
            commitDay.Color = getDayColor(commitDay.Commits.Count + 1);

        _dailyWorkVisualizerContext.Commits.Add(emptyCommit);
    }

    public IActionResult OnPostMarkAsCurrent(int id)
    {
        var task2 = _dailyWorkVisualizerContext.ToDos.Where(t => t.isCurrentTask == true).FirstOrDefault();
        if(task2 != null)
            task2.isCurrentTask = false;

        var task = _dailyWorkVisualizerContext.ToDos.FirstOrDefault(t => t.Id == id);
        if (task != null)
            task.isCurrentTask = true;

        _dailyWorkVisualizerContext.Update(task2);
        _dailyWorkVisualizerContext.Update(task);
        _dailyWorkVisualizerContext.SaveChanges();
        
        toDos = _dailyWorkVisualizerContext.ToDos.OrderByDescending(c => c.ToDoDate).ToList();

        return RedirectToPage();
    }

    public IActionResult OnPostDelete(int id)
    {
        var task = _dailyWorkVisualizerContext.ToDos.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            _dailyWorkVisualizerContext.ToDos.Remove(task);
        }
        _dailyWorkVisualizerContext.SaveChanges();
        toDos = _dailyWorkVisualizerContext.ToDos.OrderByDescending(c => c.ToDoDate).ToList();


        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostToDoAsync()
    {
        var emptyToDo = new ToDo();
        emptyToDo.ToDoDate = DateTime.Now;
        emptyToDo.Description = this.description;

        await _dailyWorkVisualizerContext.ToDos.AddAsync(emptyToDo);
        await _dailyWorkVisualizerContext.SaveChangesAsync();

        toDos = _dailyWorkVisualizerContext.ToDos.OrderByDescending(c => c.ToDoDate).ToList();

        return Page();
    }
}

