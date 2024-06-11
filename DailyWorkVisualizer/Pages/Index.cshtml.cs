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
    public void OnGet()
    {

    }

    public void OnPost()
    {
        var emptyCommit = new Commit();
        emptyCommit.CommitDate = DateTime.Today;
        emptyCommit.Description = this.description;

        _dailyWorkVisualizerContext.Commits.Add(emptyCommit);
        _dailyWorkVisualizerContext.SaveChanges();

    }
}
