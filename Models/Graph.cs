#nullable disable
namespace trackr.Models;
public class Graph
{
    public string Name { get; set; }
    public string Color { get; set; }
    public int CategoryId { get; set; }
    public string Category { get; set; }
    public List<List<string>> Data { get; set; }
}