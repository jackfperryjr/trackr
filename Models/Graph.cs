namespace Strago.Models
{
    public class Graph
    {
        public string? Name { get; set; }
        public List<List<string>>? Data { get; set; }
    }

    public class SkillView
    {
        public int Rank { get; set; }
        public string? DateLogged { get; set; }
    }
}