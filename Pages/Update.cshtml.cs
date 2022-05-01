using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using trackr.Data;
using trackr.Models;
using trackr.Extensions;
namespace trackr.Pages;

public class UpdateModel : PageModel
{
    private readonly ILogger<UpdateModel> _logger;
    private readonly trackrDbContext _context;
    [BindProperty]
    public Character Character { get; set; }
    [BindProperty]
    public string Date { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
    [BindProperty]
    public string expString { get; set; }

    public UpdateModel(ILogger<UpdateModel> logger, trackrDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void OnGet(string characterName)
    {
        Character = _context.Characters
                                    .Where(x => x.Name == characterName)
                                    .Include(x => x.Experience)
                                    .ThenInclude(x => x.Skills.OrderByDescending(y => y.DateLogged))
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
                    var name = exp.Split(":")[0];
                    var rank = Int32.Parse(exp.Split(":")[1]);
                    Skill skill = new Skill();
                    skill.ExperienceId = experience.Id;
                    skill.Name = name;
                    skill.Rank = rank;
                    skill.DateLogged = Date;
                    skill.CategoryId = ApplicationExtensions.GetCategoryId(name);
                    skills.Add(skill);
                }
            }

            _context.Skills.AddRange(skills);
        }

        _context.SaveChanges();
        return RedirectToPage("./experience", new { characterName = characterName });
    }
}
