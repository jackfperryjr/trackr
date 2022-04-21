#nullable disable
namespace Strago.Models;
public class Experience
{
    public int Id { get; set; }
    public int CharacterId { get; set; }
    public List<Skill> Skills { get; set; } = new List<Skill>();
}