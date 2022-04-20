using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Strago.Data;
using Strago.Models;

namespace Strago.Pages;

public class ExperienceModel : PageModel
{
    private readonly ILogger<ExperienceModel> _logger;
    private readonly StragoDbContext _context;
    public Character? Character { get; set; }
    public List<Graph> Graphs { get; set; } = new List<Graph>();

    public ExperienceModel(ILogger<ExperienceModel> logger, StragoDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void OnGet(string characterName)
    {
        Character = _context.Characters
                            .Include(x => x.Experience)
                            .ThenInclude(x => x.Skills)
                            .Where(x => x.Name == characterName).FirstOrDefault();
        
        foreach(var x in Character.Experience.Skills.DistinctBy(i => i.Name))
        {
            Graph graph = new Graph();
            List<List<string>> skills = new List<List<string>>();
            graph.Name = x.Name;

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

        var pause = "pause";
    }
}
