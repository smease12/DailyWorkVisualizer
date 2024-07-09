using DailyWorkVisualizer.Data;
using DailyWorkVisualizer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DailyWorkVisualizer.Pages;

public class CommitsModel : PageModel
{
    private readonly ILogger<CommitsModel> _logger;
    private readonly DailyWorkVisualizerContext _dailyWorkVisualizerContext;

    [BindProperty]
    public List<Commit> commits {get; set;}
    public List<string> dates {get; set;}

    public CommitsModel(ILogger<CommitsModel> logger, DailyWorkVisualizerContext dailyWorkVisualizerContext)
    {
        _logger = logger;
        _dailyWorkVisualizerContext = dailyWorkVisualizerContext;
    }

    public void OnGet()
    {
        commits = _dailyWorkVisualizerContext.Commits.OrderByDescending(c => c.CommitDate).ToList();
        dates = commits.Select(c =>c.CommitDate.ToString("yyyy/MM/dd")).Distinct().ToList();
    }
}

