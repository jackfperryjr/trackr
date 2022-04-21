#nullable disable
namespace trackr.Models;
public class MasterGraph
{
    public string Name { get; set; }
    public string Guild { get; set; }
    public int Circle { get; set; }
    public List<Graph> Graphs { get; set; }
}