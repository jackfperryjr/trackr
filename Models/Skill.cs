#nullable disable
namespace trackr.Models;
public class Skill
{
    public int Id { get; set; }
    public int ExperienceId { get; set; }
    public string Name { get; set; }
    public int Rank { get; set; }
    public string DateLogged { get; set; }
    public int CategoryId { get; set; }
}