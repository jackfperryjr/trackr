using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using trackr.Data;
using trackr.Models;
namespace trackr.Pages;

public class DeleteModel : PageModel
{
    private readonly ILogger<DeleteModel> _logger;
    private readonly trackrDbContext _context;
    public Character Character { get; set; }

    public DeleteModel(ILogger<DeleteModel> logger, trackrDbContext context)
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
    }

    public async Task<IActionResult> OnPostAsync(string characterName)
    {
        Character = _context.Characters
                            .Include(x => x.Experience)
                            .ThenInclude(x => x.Skills.OrderBy(x => x.CategoryId))
                            .Where(x => x.Name == characterName).FirstOrDefault();

        _context.Skills.RemoveRange(Character.Experience.Skills);
        _context.Experience.Remove(Character.Experience);
        _context.Characters.Remove(Character);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Index");
    }
}
