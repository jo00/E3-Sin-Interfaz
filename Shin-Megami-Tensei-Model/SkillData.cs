
public class SkillData
{
    public string name { get; private set; }
    public string type { get; private set; }
    public int cost { get; private set; }
    public int power { get; private set; }
    public string target { get; private set; }
    public string hits { get; private set; }
    public string effectString { get; private set; }
    
    public SkillData(string name, string type, int cost, int power, string target, string hits, string effectString)
    {
        this.name = name;
        this.type = type;
        this.cost = cost;
        this.power = power;
        this.target = target;
        this.hits = hits;
        this.effectString = effectString;
    }


}