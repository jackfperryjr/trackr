using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Strago.Data;
using Strago.Models;
using Strago.Extensions;

namespace Strago.Pages;

public class UpdateModel : PageModel
{
    private readonly ILogger<UpdateModel> _logger;
    private readonly StragoDbContext _context;
    [BindProperty]
    public Character Character { get; set; }
    [BindProperty]
    public string expString { get; set; }

    public UpdateModel(ILogger<UpdateModel> logger, StragoDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void OnGet(string characterName)
    {
        Character = _context.Characters
                                    .Where(x => x.Name == characterName)
                                    .Include(x => x.Experience)
                                    .ThenInclude(x => x.Skills)
                                    .FirstOrDefault();
    }

    public async Task<IActionResult> OnPostAsync(string characterName)
    {
        var experience = await _context.Experience.FindAsync(Character.Id);
        _context.Entry(Character).State = EntityState.Modified;

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
        return RedirectToPage("./experience", new { characterName = characterName });
    }
}
