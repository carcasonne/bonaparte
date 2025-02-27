namespace Bonaparte.Core;

public class Game
{
    public int Id { get; set; }
    
    public string Name {get; set;}
    public DateTime Created {get; set;}
    
    public ICollection<Playthrough> Playthroughs { get; set; } = new List<Playthrough>();
}