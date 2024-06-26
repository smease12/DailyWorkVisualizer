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
}

