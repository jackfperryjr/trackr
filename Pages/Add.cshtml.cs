using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Strago.Data;
using Strago.Models;
using Strago.Extensions;

namespace Strago.Pages;

public class AddModel : PageModel
{
    private readonly ILogger<AddModel> _logger;
    private readonly StragoDbContext _context;
    [BindProperty]
    public Character Character { get; set; }
    [BindProperty]
    public string expString { get; set; }

    public AddModel(ILogger<AddModel> logger, StragoDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        // TODO: Add validation around character names.
        _context.Characters.Add(Character);
        _context.SaveChanges();
        Experience experience = new Experience();
        experience.CharacterId = Character.Id;
        _context.Experience.Add(experience);
        _context.SaveChanges();

        if (expString != null) 
        {
            var expArray = ApplicationExtensions.GetExpArray(expString);
            List<Skill> skills = new List<Skill>();
            foreach(var exp in expArray)
            {
                if (exp.Length > 0)
                {
                    Skill skill = new Skill();
                    skill.ExperienceId = experience.Id;
                    skill.Name = exp.Split(":")[0];
                    skill.Rank = Int32.Parse(exp.Split(":")[1]);
                    skill.DateLogged = DateTime.Now.ToString("yyyy-MM-dd");
                    skills.Add(skill);
                }
            }

            _context.Skills.AddRange(skills);
        }

        _context.SaveChanges();
        return RedirectToPage("./experience", new { characterName = Character.Name });
    }
}
