using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Strago.Data;
using Strago.Models;
namespace Strago.Pages;

public class AllModel : PageModel
{
    private readonly ILogger<AllModel> _logger;
    private readonly StragoDbContext _context;
    public List<Character> Characters { get; set; } = new List<Character>();
    public List<MasterGraph> Graphs { get; set; } = new List<MasterGraph>();

    public AllModel(ILogger<AllModel> logger, StragoDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void OnGet()
    {
        Characters = _context.Characters
                            .Include(x => x.Experience)
                            .ThenInclude(x => x.Skills)
                            .ToList();
        
        foreach(var c in Characters)
        {
            MasterGraph master = new MasterGraph();
            List<Graph> charGraphs = new List<Graph>();

            foreach(var x in c.Experience.Skills.DistinctBy(i => i.Name))
            {
                Graph graph = new Graph();
                List<List<string>> skills = new List<List<string>>();
                graph.Name = x.Name;

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
