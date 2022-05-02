using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using trackr.Data;
using trackr.Models;
namespace trackr.Pages;

public class ExperienceModel : PageModel
{
    private readonly ILogger<ExperienceModel> _logger;
    private readonly trackrDbContext _context;
    public Character Character { get; set; }
    public List<Graph> Graphs { get; set; } = new List<Graph>();

    public ExperienceModel(ILogger<ExperienceModel> logger, trackrDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void OnGet(string characterName)
    {
        Character = _context.Characters
                            .Include(x => x.Experience)
                            .ThenInclude(x => x.Skills.OrderBy(x => x.CategoryId))
                            .Where(x => x.Name == characterName).FirstOrDefault();
        
        foreach(var x in Character.Experience.Skills.DistinctBy(i => i.Name))
        {
            var color = "#5865F2";
            switch (x.CategoryId)
            {
                case 1: color = "#fd7e14";
                break;
                case 2: color = "#dc3545";
                break;
                case 3: color = "#6610f2";
                break;
                case 4: color = "#198754";
                break;
                case 5: color = "#ffc107";
                break;
            }

            Graph graph = new Graph();
            List<List<string>> skills = new List<List<string>>();
            graph.Name = x.Name;
            graph.Color = color;
            graph.CategoryId = x.CategoryId;
            graph.Category = _context.SkillCategories
                                .Where(y => y.Id == x.CategoryId)
                                .Select(y => y.Name)
                                .FirstOrDefault();

            foreach(var y in Character.Experience.Skills.Where(i => i.Name == x.Name))
            {
                List<string> skill = new List<string>();
                skill.Add(y.DateLogged);
                skill.Add(y.Rank.ToString());
                skills.Add(skill);
            }

            graph.Data = skills;
            Graphs.Add(graph);
        }
    }
}
