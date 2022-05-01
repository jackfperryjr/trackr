using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using trackr.Data;
using trackr.Models;
namespace trackr.Pages;

public class AllModel : PageModel
{
    private readonly ILogger<AllModel> _logger;
    private readonly trackrDbContext _context;
    public List<Character> Characters { get; set; } = new List<Character>();
    public List<MasterGraph> Graphs { get; set; } = new List<MasterGraph>();

    public AllModel(ILogger<AllModel> logger, trackrDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void OnGet()
    {
        Characters = _context.Characters
                            .Include(x => x.Experience)
                            .ThenInclude(x => x.Skills.OrderBy(x => x.CategoryId))
                            .OrderByDescending(x => x.Circle)
                            .ToList();
        
        foreach(var c in Characters)
        {
            MasterGraph master = new MasterGraph();
            List<Graph> charGraphs = new List<Graph>();

            foreach(var x in c.Experience.Skills.DistinctBy(i => i.Name))
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

                foreach(var y in c.Experience.Skills.Where(i => i.Name == x.Name))
                {
                    List<string> skill = new List<string>();
                    skill.Add(y.DateLogged);
                    skill.Add(y.Rank.ToString());
                    skills.Add(skill);
                }

                graph.Data = skills;
                charGraphs.Add(graph);
            }

            master.Name = c.Name;
            master.Guild = c.Guild;
            master.Circle = c.Circle;
            master.Graphs = charGraphs;
            Graphs.Add(master);
        }
    }
}
