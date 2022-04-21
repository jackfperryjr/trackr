#nullable disable
namespace Strago.Models;
public class Character
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Circle { get; set; } = 0;
    public string Guild { get; set; } = "";
    public string Race { get; set; } = "";
    public string Gender { get; set; } = "";
    public Experience Experience { get; set; }
}