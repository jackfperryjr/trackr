using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using trackr.Data;
using trackr.Models;
namespace trackr.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly StragoDbContext _context;
    public List<Character> Characters { get; set; } = new List<Character>();

    public IndexModel(ILogger<IndexModel> logger, StragoDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void OnGet()
    {
        Characters = _context.Characters
                                    .Include(x => x.Experience)
                                    .ThenInclude(x => x.Skills)
                                    .OrderByDescending(x => x.Circle)
                                    .ToList();
    }
}
